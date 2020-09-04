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
            Assert.IsTrue(true);
        }
    }
}