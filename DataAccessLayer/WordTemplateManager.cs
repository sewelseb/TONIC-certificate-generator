using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Words;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using Models;

namespace DataAccessLayer
{
    public class WordTemplateManager : ITemplateManager
    {
        private string _templatePath;
        private string _outputDir;
        private string _currentFileName;

        public string GetTemplateFromContact(string replaced, Contact contact)
        {
            _currentFileName = contact.mail + ".docx";
            return ReplaceKey(replaced, contact.names);
        }

        public bool SetTemplateFile(string path)
        {
            if (!File.Exists(path)) return false;

            _templatePath = path;
            return true;
        }

        public bool SetOutputDir(string path)
        {
            if (!Directory.Exists(path)) return false;

            _outputDir = path;
            return true;
        }

        private string ReplaceKey(string pattern, string value)
        {
            if (_templatePath == null) throw new FileNotFoundException("Template not found");
            if (_outputDir == null) throw new DirectoryNotFoundException("Output directory not found");

            File.Copy(_templatePath, System.IO.Path.Combine(_outputDir, _currentFileName), true);

            WordprocessingDocument wordDocument = WordprocessingDocument.Open(_templatePath, false);
            var body = wordDocument.MainDocumentPart.Document.Body;
            string docText = null;
            using (StreamReader sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }
            wordDocument.Close();
            Regex regexText = new Regex(pattern);
            docText = regexText.Replace(docText, value);
            using (WordprocessingDocument cloneDocument =
                WordprocessingDocument.Open(System.IO.Path.Combine(_outputDir, _currentFileName), true))
            {
                using (StreamWriter sw = new StreamWriter(cloneDocument.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }

            return System.IO.Path.Combine(_outputDir, _currentFileName);

        }

    }
}