using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Words;
using Moq;

namespace BusinessLayer.Tests
{
    [TestClass()]
    public class WordTemplateManagerTests
    {
        private WordTemplateManager _wordTemplateManager;

        [TestInitialize]
        public void Initialize()
        {
            _wordTemplateManager = new WordTemplateManager();
        }

        [TestMethod()]
        public void GetTemplateFromContactTest_ShouldReturnHelloWorld()
        {
            var doc = new Document();
            var builder = new DocumentBuilder(doc);

            builder.Writeln("TestReplace");
            
            Assert.Fail();
        }
    }
}