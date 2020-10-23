using System;
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
            return ReplaceKey(contact);
        }

        private string ReplaceKey(Contact contact)
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
            ReplaceTextWithPattern(docText, contact);

            return Path.Combine(_outputDir, _currentFileName + ".pdf");
        }

        private void ReplaceTextWithPattern(string docText, Contact contact)
        {
            var regexText = new Regex(Regex.Escape(_config["KEYWORD_REPLACED_NAME"]));
            docText = regexText.Replace(docText, contact.Name);
            regexText = new Regex(Regex.Escape(_config["KEYWORD_REPLACED_SERIAL"]));
            docText = regexText.Replace(docText, contact.SerialNumber.ToString());
            regexText = new Regex(Regex.Escape(_config["KEYWORD_REPLACED_CONFNAME"]));
            docText = regexText.Replace(docText, _config["CONFERENCE_NAME"]);
            regexText = new Regex(Regex.Escape(_config["KEYWORD_REPLACED_CONFDATE"]));
            var Date = DateTime.Parse(_config["CONFERENCE_DATE"]);
            var stringDate = Date.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-US"));
            docText = regexText.Replace(docText, stringDate);

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