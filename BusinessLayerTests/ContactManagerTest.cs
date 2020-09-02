using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLayerTests
{
    [TestClass]
    public class ContactManagerTest
    {
        private Mock<IContactManager> _contactManagerMock;

        [TestInitialize]
        public void Initialize()
        {
            _contactManagerMock =  new Mock<IContactManager>();
            _contactManagerMock.Setup(x => x.LoadContacts()).Returns("");
        }

        // To delete after
        [TestMethod]
        public void LoadContacts_ShouldReturnAString()
        {
            var actual = _contactManagerMock.Object.LoadContacts();
            Assert.IsInstanceOfType(actual, typeof(string));
        }


    }
}
