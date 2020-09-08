using System.Collections.Generic;
using Models;

namespace DataAccessLayer
{
    public interface IMailManager
    {
        void SendEmailToContactWithAttachmnent(KeyValuePair<Contact, string> contactFilePathPair);
    }
}