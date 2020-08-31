using System;
using DataAccessLayer;

namespace BusinessLayer
{
    public class ContactManager : IContactManager
    {
        private IExcelFilesManager _excelFilesManager;

        public ContactManager(IExcelFilesManager excelFilesManager)
        {
            _excelFilesManager = excelFilesManager;
        }

        public string LoadContacts()
        {
            _excelFilesManager.GetClients();
            return "";
        }
    }
}