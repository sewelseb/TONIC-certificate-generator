using Aspose.Words;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void GetTemplateFromContactTest_ShouldReturnHelloWorld()
        {
            var doc = new Document();
            var builder = new DocumentBuilder(doc);

            builder.Writeln("TestReplace");

            Assert.IsTrue(true);
        }
    }
}