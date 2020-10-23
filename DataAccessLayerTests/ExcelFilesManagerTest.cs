using System.Collections.Generic;
using System.IO;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace DataAccessLayerTests
{
    [TestClass]
    public class ExcelFilesManagerTest
    {
        private readonly ExcelFilesManager _excelFileManagerWith2Columns = new ExcelFilesManager();
        private readonly ExcelFilesManager _excelFileManagerWith3Columns = new ExcelFilesManager();
        private readonly ExcelFilesManager _excelFileManagerWith1Contact = new ExcelFilesManager();
        private List<Contact> contactList2Columns;
        private List<Contact> contactList3Columns;
        private List<Contact> contactList1Contact;

        [TestInitialize]
        public void Initialize()
        {
            _excelFileManagerWith2Columns.SetSourceFile("testFiles/contact2columns.xlsx");
            _excelFileManagerWith3Columns.SetSourceFile("testFiles/contact3columns.xlsx");
            _excelFileManagerWith1Contact.SetSourceFile("testFiles/contactWith1contact.xlsx");
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
    }
}