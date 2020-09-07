using System.Collections.Generic;
using Models;

namespace DataAccessLayer
{
    public interface IExcelFilesManager
    {
        List<Contact> GetContacts();
        void SetSourceFile(string path);
    }
}