using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLayerTests
{
    [TestClass]
    public class ContactManagerTest
    {
        [TestMethod]
        public void LoadContacts_ShouldGetTheListOfContactFromTheExcelFile()
        {
            var contactManager = new ContactManager();

            var actual = contactManager.LoadContacts();

            Assert.IsInstanceOfType(actual, typeof(string));
        }
    }
}
