using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
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
