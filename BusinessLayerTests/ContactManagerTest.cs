using System.Collections.Generic;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace BusinessLayerTests
{
    [TestClass]
    public class ContactManagerTest
    {
        private readonly Mock<IExcelFilesManager> _excelFileManagerMock = new Mock<IExcelFilesManager>();
        private readonly Mock<ITemplateManager> _templateManagerMock = new Mock<ITemplateManager>();
        private Contact _contact;
        private IContactManager _contactManager;

        [TestInitialize]
        public void Initialize()
        {
            _contact = new Contact {Mail = "test@test.com", Name = "Test Document"};
            _templateManagerMock.Setup(x => x.GetTemplateFromContact("", _contact)).Returns("testFiles/WordTest.docx");
            _excelFileManagerMock.Setup(x => x.GetContacts()).Returns(new List<Contact> {_contact});
            _contactManager = new ContactManager(_excelFileManagerMock.Object, _templateManagerMock.Object);
        }

        [TestMethod]
        public void GetDocumentForAllContacts_ShouldReturnListOfKeyValue()
        {
            Assert.IsInstanceOfType(_contactManager.GetDocumentForAllContacts(),
                typeof(List<KeyValuePair<Contact, string>>));
        }

        [TestMethod]
        public void SetSourceFile_ShouldSetupSourceFile()
        {
            _contactManager.SetSourceFile("testFiles/contact.xlsx");
        }

        [TestMethod]
        public void SetOutputDir_ShouldSetupSetupDir()
        {
            _contactManager.SetOutputDir("testFiles");
        }

        [TestMethod]
        public void SetTemplateFile_ShouldSetupTemplateFile()
        {
            _contactManager.SetTemplateFile("testFiles/WordTest.docx");
        }
    }
}