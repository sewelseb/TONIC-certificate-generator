using System.IO;
using DataAccessLayer;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace BusinessLayer.Tests
{
    [TestClass]
    public class WordTemplateManagerTests
    {
        private WordTemplateManager _wordTemplateManager;

        [TestInitialize]
        public void Initialize()
        {
            _wordTemplateManager = new WordTemplateManager();
        }

        [TestMethod]
        public void SetTemplateFIle_ShouldSetupFilePath()
        {
            var path = "testFiles/WordTest.docx";
            _wordTemplateManager.SetTemplateFile(path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SetTemplateFIle_ShouldThrowFileNotFoundException()
        {
            var path = "wrongPath";
            _wordTemplateManager.SetTemplateFile(path);
        }

        [TestMethod]
        public void SetOutputDir_ShouldSetupOutputDirPath()
        {
            var path = "testFiles";
            _wordTemplateManager.SetOutputDir(path);
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void SetOutputDir_ShouldThrowDirectoryNotFoundException()
        {
            var path = "wrongPath";
            _wordTemplateManager.SetOutputDir(path);
        }


        [TestMethod]
        public void GetTemplateFromContactTest_ShouldReturnHelloWorld()
        {
            _wordTemplateManager.SetOutputDir("testFiles/");
            _wordTemplateManager.SetTemplateFile("testFiles/WordTest.docx");
            var docText = GetTextFromWordDoc();
            Assert.IsTrue(docText.Contains("Hello World"));
        }

        private string GetTextFromWordDoc()
        {
            var path = _wordTemplateManager.GetTemplateFromContact("Test Document",
                new Contact {Mail = "test@test.com", Name = "Hello World"});
            var wordDocument = WordprocessingDocument.Open(path, false);
            string docText = null;
            using (var sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }

            wordDocument.Close();
            return docText;
        }
    }
}