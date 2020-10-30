using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Serilog;

namespace DataAccessLayerTests
{
    [TestClass]
    public class ExcelFilesManagerTest
    {
        private ILogger _mockedLogger;
        private ExcelFilesManager _excelFileManagerWith2Columns;
        private ExcelFilesManager _excelFileManagerWith3Columns;
        private ExcelFilesManager _excelFileManagerWith1Contact;
        private ExcelFilesManager _excelFileManagerWithEmailsError;


        [TestInitialize]
        public void Initialize()
        {
            _mockedLogger = new Mock<ILogger>().Object;
            _excelFileManagerWith2Columns = new ExcelFilesManager(_mockedLogger);
            _excelFileManagerWith3Columns = new ExcelFilesManager(_mockedLogger);
            _excelFileManagerWith1Contact = new ExcelFilesManager(_mockedLogger);
            _excelFileManagerWithEmailsError = new ExcelFilesManager(_mockedLogger);
            _excelFileManagerWith2Columns.SetSourceFile("testFiles/contact2columns.xlsx");
            _excelFileManagerWith3Columns.SetSourceFile("testFiles/contact3columns.xlsx");
            _excelFileManagerWith1Contact.SetSourceFile("testFiles/contactWith1contact.xlsx");
            _excelFileManagerWithEmailsError.SetSourceFile("testFiles/contact2columnsWithEmailsError.xlsx");
        }

        [TestMethod]
        public void GetContact_ShouldReturnAContactListTypeObject()
        {
            var contactListObject2Columns = _excelFileManagerWith2Columns.GetContacts();
            var contactListObject3Columns = _excelFileManagerWith3Columns.GetContacts();
            var contactListObject1Contact = _excelFileManagerWith1Contact.GetContacts();
            var contactListObjectError = _excelFileManagerWith1Contact.GetContacts();

            Assert.IsInstanceOfType(contactListObject2Columns, typeof(List<Contact>));
            Assert.IsInstanceOfType(contactListObject3Columns, typeof(List<Contact>));
            Assert.IsInstanceOfType(contactListObject1Contact, typeof(List<Contact>));
            Assert.IsInstanceOfType(contactListObjectError, typeof(List<Contact>));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SetSourceFile_ShouldThrowFileNotFoundException()
        {
            _excelFileManagerWith2Columns.SetSourceFile("wrongPath");
            _excelFileManagerWith3Columns.SetSourceFile("wrongPath");
            _excelFileManagerWith1Contact.SetSourceFile("wrongPath");
            _excelFileManagerWithEmailsError.SetSourceFile("wrongPath");
        }

        [TestMethod]
        public void GetContact_ContactNamesAndEmailsShouldHaveACompleteString()
        {
            var contactList2Columns = _excelFileManagerWith2Columns.GetContacts();
            var contactList3Columns = _excelFileManagerWith3Columns.GetContacts();
            var contactList1Contact = _excelFileManagerWith1Contact.GetContacts();
            var contactListError = _excelFileManagerWith1Contact.GetContacts();


            Assert.IsTrue(contactList2Columns[0].Mail.Length >= 0 && contactList2Columns[0].Name.Length >= 0);
            Assert.IsTrue(contactList3Columns[0].Mail.Length >= 0 && contactList3Columns[0].Name.Length >= 0);
            Assert.IsTrue(contactList1Contact[0].Mail.Length >= 0 && contactList1Contact[0].Name.Length >= 0);
            Assert.IsTrue(contactListError[0].Mail.Length >= 0 && contactListError[0].Name.Length >= 0);
        }

        [TestMethod]
        public void GetContact_ShouldReturnJeanMarcelleAsAFirstString()
        {
            var contactList2Columns = _excelFileManagerWith2Columns.GetContacts();
            var contactList3Columns = _excelFileManagerWith3Columns.GetContacts();
            var contactList1Contact = _excelFileManagerWith1Contact.GetContacts();
            var contactListError = _excelFileManagerWith1Contact.GetContacts();

            var name = contactList2Columns[0].Name;
            var name2 = contactList3Columns[0].Name;
            var name3 = contactList1Contact[0].Name;
            var name4 = contactListError[0].Name;

            Assert.IsTrue(name == "jean marcelle" && name2 == "jean marcelle" && name3 == "jean marcelle" && name4 == "jean marcelle");
        }

        [TestMethod]
        public void GetContact_ContactFileWithErrorsShouldReturnTwoElements()
        {
            var numberOfContacts = _excelFileManagerWithEmailsError.GetContacts().Count;

            Assert.AreEqual(numberOfContacts, 2);
        }
    }

}
