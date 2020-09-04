using System.Collections.Generic;
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
            _excelFileManager.SetSourceFile("testFiles/contact.xlsx");
            _excelFileManagerMock = new Mock<IExcelFilesManager>();
            _excelFileManagerMock
                .Setup(x => x.GetContacts())
                .Returns(new List<Contact>());
        }

        [TestMethod]
        public void LoadExcel_ShouldReturnAContactList()
        {
            //List<Contact> actualList = _excelFileManagerMock.Object.GetContacts();
            var actualList = _excelFileManager.GetContacts();
            Assert.IsInstanceOfType(actualList, typeof(List<Contact>));
        }
    }
}