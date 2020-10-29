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
        private List<Contact> contactList2Columns;
        private List<Contact> contactList3Columns;
        private List<Contact> contactList1Contact;

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
            contactList2Columns = _excelFileManagerWith2Columns.GetContacts();
            contactList3Columns = _excelFileManagerWith3Columns.GetContacts();
            contactList1Contact = _excelFileManagerWith1Contact.GetContacts();
        }

        [TestMethod]
        public void GetContact_ShouldReturnAContactList()
        {
            Assert.IsInstanceOfType(contactList2Columns, typeof(List<Contact>));
            Assert.IsInstanceOfType(contactList3Columns, typeof(List<Contact>));
            Assert.IsInstanceOfType(contactList1Contact, typeof(List<Contact>));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SetSourceFile_ShouldThrowFileNotFoundException()
        {
            _excelFileManagerWith2Columns.SetSourceFile("wrongPath");
            _excelFileManagerWith3Columns.SetSourceFile("wrongPath");
            _excelFileManagerWith1Contact.SetSourceFile("wrongPath");
        }

        [TestMethod]
        public void GetContact_ShouldReturnAFullString()
        {
            Assert.IsTrue(contactList2Columns[0].Mail.Length >= 0 && contactList2Columns[0].Name.Length >= 0);
            Assert.IsTrue(contactList3Columns[0].Mail.Length >= 0 && contactList3Columns[0].Name.Length >= 0);
            Assert.IsTrue(contactList1Contact[0].Mail.Length >= 0 && contactList1Contact[0].Name.Length >= 0);
        }

        [TestMethod]
        public void ShouldReturnJeanMarcelleAsAFirstString()
        {
            var name = contactList2Columns[0].Name;
            var name2 = contactList3Columns[0].Name;
            var name3 = contactList1Contact[0].Name;

            Assert.IsTrue(name == "jean marcelle" && name2 == "jean marcelle" && name3 == "jean marcelle");
        }

        [TestMethod]
        public void ShouldReturnTwoElements()
        {
            var contacts = _excelFileManagerWithEmailsError.GetContacts().Count;
            Assert.IsTrue(contacts == 2);
        }
    }

}
