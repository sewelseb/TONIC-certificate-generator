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
            SetupServices(args);

            Console.WriteLine("Hello World!");
        }

        private static void SetupServices(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IContactManager, ContactManager>()
                .AddSingleton<ISendinBlueConnector, SendinBlueConnector>()
                .AddSingleton<IExcelFilesManager, ExcelFilesManager>()
                .AddSingleton<IInputParser, InputParser>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var parser = serviceProvider.GetService<IInputParser>();

        }
    }
}
