using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Models;
using Scriban;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;

namespace DataAccessLayer
{
    public class SendinBlueManager : IMailManager
    {
        private readonly IConfigurationRoot _config;
        private readonly SmtpClient _smtp;

        public SendinBlueManager(IConfigurationRoot config)
        {
            _config = config;
            ApiKey = _config["MAIL_API_KEY"];
            Configuration.Default.ApiKey.Add("api-key", ApiKey);
            _smtp = new SmtpClient(_config["SMTP_SERVER"], int.Parse(_config["SMTP_PORT"]));
            _smtp.EnableSsl = true;
            _smtp.Credentials = new NetworkCredential(_config["SMTP_LOGIN"], _config["SMTP_PASSWORD"]);
        }

        public string ApiKey { get; set; }


        public void SendEmailToContactWithAttachmnent(KeyValuePair<Contact, string> contactFilePathPair)
        {
            var apiInstance = new SMTPApi();
            var getTemplate = apiInstance.GetSmtpTemplate(int.Parse(_config["SMTP_TEMPLATE_ID"]));
            var from = _config["SMTP_LOGIN"];
            var to = contactFilePathPair.Key.Mail;
            var mail = new MailMessage(from, to);
            mail.Attachments.Add(new Attachment(contactFilePathPair.Value));
            var template = Template.Parse(getTemplate.HtmlContent);
            var result = template.Render(contactFilePathPair.Key);
            mail.Body = result;
            mail.IsBodyHtml = true;
            _smtp.Send(mail);
        }

        private bool getAccountConnection()
        {
            return true;
        }
    }
}