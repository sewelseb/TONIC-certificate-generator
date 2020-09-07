using System;
using System.IO;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TonicCertificateGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = SetupServices();
            var contactManager = (IContactManager) serviceProvider.GetService(typeof(IContactManager));
            contactManager.SetSourceFile(
                @"F:\pierr\Projet\TONIC-certificate-generator\TonicCertificateGenerator\contact.xlsx");
            contactManager.SetTemplateFile(
                @"F:\pierr\Projet\TONIC-certificate-generator\TonicCertificateGenerator\WordTest.docx");
            contactManager.SetOutputDir(@"F:\pierr\Projet\TONIC-certificate-generator\output");
            var listContactFilepathPair = contactManager.GetDocumentForAllContacts();
            var mailManager = (IMailManager) serviceProvider.GetService(typeof(IMailManager));
            foreach (var contactFilepathPair in listContactFilepathPair)
                mailManager.SendEmailToContactWithAttachmnent(contactFilepathPair);
            Console.WriteLine("Hello World!");
        }

        private static IServiceProvider SetupServices()
        {
            var configuration = SetupConfiguration();
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IContactManager, ContactManager>()
                .AddSingleton<IExcelFilesManager, ExcelFilesManager>()
                .AddSingleton<ITemplateManager, WordTemplateManager>()
                .AddSingleton<IMailManager, SendinBlueManager>()
                .AddSingleton(configuration);

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfigurationRoot SetupConfiguration()
        {
            IConfigurationRoot configuration;
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            return configuration;
        }
    }
}