using System;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace TonicCertificateGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupServices();

            Console.WriteLine("Hello World!");
        }

        private static void SetupServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IContactManager, ContactManager>()
                .AddSingleton<ISendinBlueConnector, SendinBlueConnector>()
                .AddSingleton<IExcelFilesManager, ExcelFilesManager>();

            serviceProvider.BuildServiceProvider();
        }
    }
}
