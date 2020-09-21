using System;
using System.IO;
using BusinessLayer;
using CommandLine;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TonicCertificateGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                if (!File.Exists(o.ConfigFile)) throw new FileNotFoundException("Config file not found");

                var serviceProvider = SetupServices(o);
                var contactManager = (IContactManager) serviceProvider.GetService(typeof(IContactManager));
                var config = (IConfigurationRoot) serviceProvider.GetService(typeof(IConfigurationRoot));
                contactManager.SetSourceFile(config["SOURCE_PATH"]);
                contactManager.SetTemplateFile(config["TEMPLATE_PATH"]);
                contactManager.SetOutputDir(config["OUTPUT_DIR"]);
                var listContactFilepathPair = contactManager.GetDocumentForAllContacts();
                //var mailManager = (IMailManager) serviceProvider.GetService(typeof(IMailManager));
                //foreach (var contactFilepathPair in listContactFilepathPair)
                //    mailManager.SendEmailToContactWithAttachmnent(contactFilepathPair);
            });
        }

        private static IServiceProvider SetupServices(Options options)
        {
            var configuration = SetupConfiguration(options);
            var logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs.txt").CreateLogger();
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IContactManager, ContactManager>()
                .AddSingleton<IExcelFilesManager, ExcelFilesManager>()
                .AddSingleton<ITemplateManager, WordTemplateManager>()
                .AddSingleton<IMailManager, SendinBlueManager>()
                .AddSingleton<IInventoryManager, InventoryCsvManager>()
                .AddSingleton<ILogger>(logger)
                .AddSingleton(configuration);

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfigurationRoot SetupConfiguration(Options options)
        {
            IConfigurationRoot configuration;
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile(options.ConfigFile, false)
                .Build();
            return configuration;
        }

        public class Options
        {
            [Option('c', "config_file", Required = true, HelpText = "Configuration file in json format")]
            public string ConfigFile { get; set; }
        }
    }
}