using System;
using System.Collections.Generic;
using Mandrill;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer
{
    public class MailChimpManager : IMailManager
    {
        private readonly MandrillApi _mandrillInterface;

        public MailChimpManager(IConfigurationRoot config)
        {
            _mandrillInterface = new MandrillApi(config["MAIL_API_KEY"]);
        }

        public void SendEmailToContactWithAttachmnent(KeyValuePair<Contact, string> contactFilePathPair)
        {
            throw new NotImplementedException();
        }
    }
}