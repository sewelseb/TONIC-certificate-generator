using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocXToPdfConverter;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer
{
    public class WordTemplateManager : ITemplateManager
    {
        private readonly IConfigurationRoot _config;
        private string _currentFileName;
        private string _outputDir;
        private string _templatePath;

        public WordTemplateManager(IConfigurationRoot config)
        {
            _config = config;
        }

        public void SetTemplateFile(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();

            _templatePath = path;
        }

        public void SetOutputDir(string path)
        {
            if (!Directory.Exists(path)) throw new DirectoryNotFoundException();

            _outputDir = path;
        }

        public string GetTemplateFromContact(Contact contact)
        {
            _currentFileName = contact.Mail;
            return ReplaceKey(Regex.Escape(_config["KEYWORD_REPLACED"]), contact.Name);
        }

        private string ReplaceKey(string pattern, string value)
        {
            if (_templatePath == null) throw new FileNotFoundException("Template not found");
            if (_outputDir == null) throw new DirectoryNotFoundException("Output directory not found");

            File.Copy(_templatePath, Path.Combine(_outputDir, _currentFileName + ".docx"), true);
            var wordDocument = WordprocessingDocument.Open(_templatePath, false);
            string docText = null;
            using (var sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }

            wordDocument.Close();
            ReplaceTextWithPattern(pattern, value, docText);

            return Path.Combine(_outputDir, _currentFileName + ".pdf");
        }

        private void ReplaceTextWithPattern(string pattern, string value, string docText)
        {
            var regexText = new Regex(pattern);
            docText = regexText.Replace(docText, value);
            using var cloneDocument =
                WordprocessingDocument.Open(Path.Combine(_outputDir, _currentFileName + ".docx"), true);
            using (var sw = new StreamWriter(cloneDocument.MainDocumentPart.GetStream(FileMode.Create)))
            {
                sw.Write(docText);
            }

            cloneDocument.Close();

            ExportToPDF();
        }

        public virtual void ExportToPDF()
        {
            var pdfOutput = Path.Combine(_outputDir, _currentFileName + ".pdf");
            var converter = new ReportGenerator(_config["LIBREOFFICE_EXEC_PATH"]);
            converter.Convert(Path.Combine(_outputDir, _currentFileName + ".docx"), pdfOutput);
        }
    }
}