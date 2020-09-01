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

        private ExcelFilesManager _excelFileManager = new ExcelFilesManager(@"D:\SUPINFO\STAGE\TonicTeaching\TONIC-certificate-generator\contact.xlsx");

        [TestInitialize]
        public void Initialize()
        {
            _excelFileManagerMock = new Mock<IExcelFilesManager>();
            _excelFileManagerMock
                .Setup(x => x.GetClients())
                .Returns("Test");
            _excelFileManagerMock.Setup(x => x.GetCellValue()).Returns("jean");
            _contactManager = new ContactManager(_excelFileManagerMock.Object);
        }

        #region BUSINESS LAYER

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

        #endregion



        #region DATA ACCESS LAYER


        [TestMethod]
        public void LoadExcel_ShouldReturnJeanAsAString()
        {
            string name = _excelFileManagerMock.Object.GetCellValue();
            //string name = _excelFileManager.GetCellValue("jean");

            Assert.AreEqual("jean", name);
        }



        #endregion


    }
}
