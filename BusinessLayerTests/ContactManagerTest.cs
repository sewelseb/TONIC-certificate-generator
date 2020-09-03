using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
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
        }

    }
}
