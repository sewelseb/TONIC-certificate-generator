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
            _contactManagerMock = new Mock<IContactManager>();
        }
    }
}