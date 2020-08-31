using BusinessLayer;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLayerTests
{
    [TestClass]
    public class ContactManagerTest
    {
        [TestMethod]
        public void LoadContacts_ShouldReturnAString()
        {
            var ExcelFileManagerMock = new Mock<IExcelFilesManager>();
            ExcelFileManagerMock
                .Setup(x => x.GetClients())
                .Returns("Test");
            var contactManager = new ContactManager(ExcelFileManagerMock.Object);

            var actual = contactManager.LoadContacts();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void LoadContacts_ShouldGetContactFromExcelFileManager()
        {
            var ExcelFileManagerMock = new Mock<IExcelFilesManager>();
            ExcelFileManagerMock
                .Setup(x => x.GetClients())
                .Returns("Test");
            var contactManager = new ContactManager(ExcelFileManagerMock.Object);

            var actual = contactManager.LoadContacts();

            ExcelFileManagerMock.Verify(x => x.GetClients(), Times.Once);
        }
    }
}
