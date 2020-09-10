using System;
using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLayer
{
    public class ContactManager : IContactManager
    {
        private readonly IExcelFilesManager _excelFilesManager;
        private readonly ITemplateManager _templateManager;
        private ILogger _logger = new Logger();

        public ContactManager(IExcelFilesManager excelFilesManager, ITemplateManager templateManager)
        {
            _excelFilesManager = excelFilesManager;
            _templateManager = templateManager;
        }

        public void SetSourceFile(string path)
        {
            _excelFilesManager.SetSourceFile(path);
            _logger.LogInformation("Excel source file is set up.");
        }

        public void SetTemplateFile(string path)
        {
            _templateManager.SetTemplateFile(path);
            _logger.LogInformation("Template file is set up.");
        }

        public void SetOutputDir(string path)
        {
            _templateManager.SetOutputDir(path);
            _logger.LogInformation($"Output directory is set up at {path}");
        }

        public List<KeyValuePair<Contact, string>> GetDocumentForAllContacts()
        {
            var contactsDocuments = new List<KeyValuePair<Contact, string>>();
            var contacts = _excelFilesManager.GetContacts();
            foreach (var contact in contacts)
            {
                contactsDocuments.Add(new KeyValuePair<Contact, string>(contact, GetDocumentForContact(contact))); 
                _logger.LogInformation($"Template succeful generated for {contact.Name} / {contact.Mail}");
            }
            return contactsDocuments;
        }

        private string GetDocumentForContact(Contact contact)
        {
            return _templateManager.GetTemplateFromContact(contact);
        }
    }
}