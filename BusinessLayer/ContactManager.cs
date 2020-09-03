using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Words;
using DataAccessLayer;
using DocumentFormat.OpenXml.Packaging;
using Models;

namespace BusinessLayer
{
    public class ContactManager : IContactManager
    {
        private IExcelFilesManager _excelFilesManager;
        private ITemplateManager _templateManager;

        public void SetSourceFile(string path)
        {
            var source = _excelFilesManager.SetSourceFile(path);

        }
        public void SetTemplateFile(string path)
        {
            _templateManager.SetTemplateFile(path);
        }

        public void SetOutputDir(string path)
        {
            _templateManager.SetOutputDir(path);
        }

        public ContactManager(IExcelFilesManager excelFilesManager, ITemplateManager templateManager)
        {
            _excelFilesManager = excelFilesManager;
            _templateManager = templateManager;
        }

        public Dictionary<Contact, string> GetDocumentForAllContacts()
        {
            var contactsDocuments = new Dictionary<Contact, string>();
            var contacts = _excelFilesManager.GetContacts();
            foreach (var contact in contacts)
            {
                contactsDocuments.Add(contact, GetDocumentForContact(contact));
            }

            return contactsDocuments;
        }

        private string GetDocumentForContact(Contact contact)
        {
            return _templateManager.GetTemplateFromContact("name", contact);
        }
    }
}