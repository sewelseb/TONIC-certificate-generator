using Models;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IExcelFilesManager
    {
        List<Contact> GetContacts();
        bool SetSourceFile(string path);
    }
}
