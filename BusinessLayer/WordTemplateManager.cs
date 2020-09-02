using System;
using Aspose.Words;
using Models;

namespace BusinessLayer
{
    public class WordTemplateManager : ITemplateManager
    {
        private IContactManager _contactManager;
        private Document _wordDocument;

        public string Path { get; set; }

        public WordTemplateManager(IContactManager contactManager, string path)
        {
            this._contactManager = contactManager;
            this._wordDocument = new Document(path);
        }

        public Object GetTemplateFromContact(string replaced, Contact contact)
        {
            return this.ReplaceKey(replaced, contact.names);
        }

        private Document ReplaceKey(string replaced, string value)
        {
            Document clone = this._wordDocument.Clone();
            clone.Range.Replace(replaced, value);
            return clone;

        }

    }
}