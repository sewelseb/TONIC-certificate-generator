using System.Collections.Generic;
using Models;

namespace BusinessLayer
{
    public interface IContactManager
    {
        void SetSourceFile(string path);
        void SetTemplateFile(string path);
        void SetOutputDir(string path);
        Dictionary<Contact, string> GetDocumentForAllContacts();
    }
}