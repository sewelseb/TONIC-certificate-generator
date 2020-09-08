using System.IO;
using DataAccessLayer;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace BusinessLayer.Tests
{
    [TestClass]
    public class WordTemplateManagerTests
    {
        private IConfigurationRoot _mockedConfig;
        private WordTemplateManager _wordTemplateManager;

        [TestInitialize]
        public void Initialize()
        {
            var configMock = new Mock<IConfigurationRoot>();
            configMock.SetupGet(x => x["KEYWORD_REPLACED"]).Returns("Test Document");
            _mockedConfig = configMock.Object;
            _wordTemplateManager = new WordTemplateManager(_mockedConfig);
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
            var mockManager = new Mock<WordTemplateManager>(_mockedConfig);
            mockManager.Setup(x => x.ExportToPDF()).Verifiable();
            _wordTemplateManager = mockManager.Object;
            _wordTemplateManager.SetOutputDir("testFiles/");
            _wordTemplateManager.SetTemplateFile("testFiles/WordTest.docx");

            var docText = GetTextFromWordDoc();

            Assert.IsTrue(docText.Contains("Hello World"));
        }

        private string GetTextFromWordDoc()
        {
            _wordTemplateManager.GetTemplateFromContact(
                new Contact {Mail = "test@test.com", Name = "Hello World"});
            var wordDocument = WordprocessingDocument.Open("testFiles/test@test.com.docx", false);
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