using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace DataAccessLayer.Tests
{
    [TestClass]
    public class MailManagerTests
    {
        private Mock<IConfigurationRoot> _configMock;
        private IMailManager _mailManager;
        private JsonArrayAttribute jsonConfig;

        [TestInitialize]
        public void Initialize()
        {
            //_configMock = new Mock<IConfigurationRoot>();
            //_configMock.SetupGet(x => x["SENDINBLUE_API_KEY"]).Returns("Test");
            //_mailManager = new SendinBlue(_configMock.Object);
        }


        [TestMethod]
        public void MailManagerTest()
        {
        }

        [TestMethod]
        public void sendEmailTest()
        {
            Assert.Fail();
        }
    }
}