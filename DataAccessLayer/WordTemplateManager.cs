﻿using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using Models;

namespace DataAccessLayer
{
    public class WordTemplateManager : ITemplateManager
    {
        private string _currentFileName;
        private string _outputDir;
        private string _templatePath;

        public string GetTemplateFromContact(string replaced, Contact contact)
        {
            _currentFileName = contact.Mail + ".docx";
            return ReplaceKey(replaced, contact.Name);
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

            File.Copy(_templatePath, Path.Combine(_outputDir, _currentFileName), true);

            var wordDocument = WordprocessingDocument.Open(_templatePath, false);
            var body = wordDocument.MainDocumentPart.Document.Body;
            string docText = null;
            using (var sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }

            wordDocument.Close();
            var regexText = new Regex(pattern);
            docText = regexText.Replace(docText, value);
            using (var cloneDocument =
                WordprocessingDocument.Open(Path.Combine(_outputDir, _currentFileName), true))
            {
                using (var sw = new StreamWriter(cloneDocument.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }

            return Path.Combine(_outputDir, _currentFileName);
        }
    }
}