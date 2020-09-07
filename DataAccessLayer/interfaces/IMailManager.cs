using System.Collections.Generic;
using Models;

namespace DataAccessLayer
{
    public interface IMailManager
    {
        void SendEmail();
        void SendEmailTest();

        void SendEmailToContactWithAttachmnent(KeyValuePair<Contact, string> contactFilePathPair);
    }
}