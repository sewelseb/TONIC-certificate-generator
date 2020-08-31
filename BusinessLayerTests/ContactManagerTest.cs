using BusinessLayer;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLayerTests
{
    [TestClass]
    public class ContactManagerTest
    {
        private IContactManager _contactManager;
        private Mock<IExcelFilesManager> _excelFileManagerMock;

        [TestInitialize]
        public void Initialize()
        {
            _excelFileManagerMock = new Mock<IExcelFilesManager>();
            _excelFileManagerMock
                .Setup(x => x.GetClients())
                .Returns("Test");
            _contactManager = new ContactManager(_excelFileManagerMock.Object);
        }

        [TestMethod]
        public void LoadContacts_ShouldReturnAString()
        {
            var actual = _contactManager.LoadContacts();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void LoadContacts_ShouldGetContactFromExcelFileManager()
        {
            var actual = _contactManager.LoadContacts();

            _excelFileManagerMock.Verify(x => x.GetClients(), Times.Once);
        }
    }
}
