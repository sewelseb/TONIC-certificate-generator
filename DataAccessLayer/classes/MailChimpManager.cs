using System;
using System.Collections.Generic;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
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

        public void SendEmail()
        {
            throw new NotImplementedException();
        }

        public void SendEmailToContactWithAttachmnent(KeyValuePair<Contact, string> contactFilePathPair)
        {
        }

        public void SendEmailTest()
        {
            var message = new EmailMessage();
            var adresses = new List<EmailAddress>
            {
                new EmailAddress("pierrevob@hotmail.com"),
                new EmailAddress("leble17@gmail.com")
            };
            message.Text = "This is a test";
            message.To = adresses;

            var messageRequest = new SendMessageRequest(message);
            var result = _mandrillInterface.SendMessage(messageRequest);
        }
    }
}