using System.Collections.Generic;
using DataAccessLayer;
using Models;
using Serilog;

namespace BusinessLayer
{
    public class ContactManager : IContactManager
    {
        private readonly IExcelFilesManager _excelFilesManager;
        private readonly IInventoryManager _inventoryManager;
        private readonly ILogger _logger;
        private readonly ITemplateManager _templateManager;

        public ContactManager(IExcelFilesManager excelFilesManager, ITemplateManager templateManager,
            IInventoryManager inventoryManager, ILogger logger)
        {
            _excelFilesManager = excelFilesManager;
            _templateManager = templateManager;
            _logger = logger;
            _inventoryManager = inventoryManager;
        }

        public void SetSourceFile(string path)
        {
            _excelFilesManager.SetSourceFile(path);
            _logger.Information("Excel source file is set up.");
        }

        public void SetTemplateFile(string path)
        {
            _templateManager.SetTemplateFile(path);
            _logger.Information("Template file is set up.");
        }

        public void SetOutputDir(string path)
        {
            _templateManager.SetOutputDir(path);
            _logger.Information($"Output directory is set up at {path}");
        }

        public List<KeyValuePair<Contact, string>> GetDocumentForAllContacts()
        {
            var contactsDocuments = new List<KeyValuePair<Contact, string>>();
            var contacts = _excelFilesManager.GetContacts();
            foreach (var contact in contacts)
            {
                var newContact = _inventoryManager.AddSerialNumber(contact);
                contactsDocuments.Add(new KeyValuePair<Contact, string>(newContact, GetDocumentForContact(newContact)));
                _logger.Information(
                    $"Template successfuly generated for {newContact.Name} / {newContact.Mail} / {newContact.SerialNumber}");
            }

            return contactsDocuments;
        }

        private string GetDocumentForContact(Contact contact)
        {
            return _templateManager.GetTemplateFromContact(contact);
        }
    }
}