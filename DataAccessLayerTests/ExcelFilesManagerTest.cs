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
        private readonly ExcelFilesManager _excelFileManager = new ExcelFilesManager();
        private Mock<IExcelFilesManager> _excelFileManagerMock;

        [TestInitialize]
        public void Initialize()
        {
            _excelFileManagerMock = new Mock<IExcelFilesManager>();
            _excelFileManagerMock
                .Setup(x => x.GetContacts())
                .Returns(new List<Contact>());
            _excelFileManager.SetSourceFile("testFiles/contact.xlsx");
        }

        [TestMethod]
        public void GetContact_ShouldReturnAContactList()
        {
            var actualList = _excelFileManager.GetContacts();

            Assert.IsInstanceOfType(actualList, typeof(List<Contact>));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SetSourceFile_ShouldThrowFileNotFoundException()
        {
            _excelFileManager.SetSourceFile("wrongPath");
        }


        [TestMethod]
        public void GetContact_ShouldReturnAFullString()
        {
            var actualList = _excelFileManager.GetContacts();

            Assert.IsTrue(actualList[0].Mail.Length >= 0 && actualList[0].Name.Length >= 0);
        }
    }
}