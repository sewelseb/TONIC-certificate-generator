using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLayer
{
    public class ContactManager : IContactManager
    {
        private readonly IExcelFilesManager _excelFilesManager;
        private readonly ITemplateManager _templateManager;

        public ContactManager(IExcelFilesManager excelFilesManager, ITemplateManager templateManager)
        {
            _excelFilesManager = excelFilesManager;
            _templateManager = templateManager;
        }

        public void SetSourceFile(string path)
        {
            _excelFilesManager.SetSourceFile(path);
        }

        public void SetTemplateFile(string path)
        {
            _templateManager.SetTemplateFile(path);
        }

        public void SetOutputDir(string path)
        {
            _templateManager.SetOutputDir(path);
        }

        public List<KeyValuePair<Contact, string>> GetDocumentForAllContacts()
        {
            var contactsDocuments = new List<KeyValuePair<Contact, string>>();
            var contacts = _excelFilesManager.GetContacts();
            foreach (var contact in contacts)
                contactsDocuments.Add(new KeyValuePair<Contact, string>(contact, GetDocumentForContact(contact)));

            return contactsDocuments;
        }

        private string GetDocumentForContact(Contact contact)
        {
            return _templateManager.GetTemplateFromContact(contact);
        }
    }
}