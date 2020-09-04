using System;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace TonicCertificateGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SetupServices();
            Console.WriteLine("Hello World!");
        }

        private static void SetupServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IContactManager, ContactManager>()
                .AddSingleton<ISendinBlueConnector, SendinBlueConnector>()
                .AddSingleton<IExcelFilesManager, ExcelFilesManager>()
                .AddSingleton<ITemplateManager, WordTemplateManager>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var contactManager = (IContactManager) serviceProvider.GetService(typeof(IContactManager));
            contactManager.SetSourceFile(@"F:\pierr\Projet\TONIC-certificate-generator\contact.xlsx");
            contactManager.SetTemplateFile(@"F:\pierr\Projet\TONIC-certificate-generator\WordTest.docx");
            contactManager.SetOutputDir(@"F:\pierr\Projet\TONIC-certificate-generator\output");
            var contactsDocuments = contactManager.GetDocumentForAllContacts();
        }
    }
}