using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Serilog;

namespace BusinessLayer.Tests
{
    [TestClass]
    public class InventoryCsvManagerTests
    {
        private InventoryCsvManager _inventoryCsvManager;
        private IConfigurationRoot _mockedConfig;

        [TestInitialize]
        public void Initialize()
        {
            var configMock = new Mock<IConfigurationRoot>();
            configMock.SetupGet(x => x["INVENTORY_PATH"])
                .Returns(Path.Combine(Directory.GetCurrentDirectory(), "inventoryTest.csv"));
            _mockedConfig = configMock.Object;
            var mockedLogger = new Mock<ILogger>().Object;
            _inventoryCsvManager = new InventoryCsvManager(_mockedConfig, mockedLogger);
        }

        [TestMethod]
        public void GetLastSerialNumber_ShouldReturnInt()
        {
            Assert.IsInstanceOfType(_inventoryCsvManager.GetLastSerialNumber(), typeof(long));
        }

        [TestMethod]
        public void AddSerialNumber_ShouldAddTheCorrectSerialNumber()
        {
            var contact = new Contact {Name = "Jean", Mail = "marcelle@jean.be"};

            _inventoryCsvManager.AddSerialNumber(contact);
            var _reverseStream = new ReverseLineReader(_mockedConfig["INVENTORY_PATH"]).Take(2);
            var current = _reverseStream.First().Split(',')[0];
            var previous = _reverseStream.Last().Split(',')[0];

            Assert.AreEqual(long.Parse(current), long.Parse(previous) + 1);
        }
    }
}