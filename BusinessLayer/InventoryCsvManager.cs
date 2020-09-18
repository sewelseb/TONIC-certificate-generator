using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Models;
using Serilog;

namespace BusinessLayer
{
    public class InventoryCsvManager : IInventoryManager
    {
        private readonly IConfigurationRoot _config;
        private ILogger _logger;

        public InventoryCsvManager(IConfigurationRoot config, ILogger logger)
        {
            if (!File.Exists(config["INVENTORY_PATH"])) throw new FileNotFoundException();

            _config = config;
            _logger = logger;
        }

        public void AddSerialNumber(Contact contact)
        {
            var current = GetLastSerialNumber() + 1;
            using (var sw = new StreamWriter(_config["INVENTORY_PATH"], true))
            {
                var line = "\n" + current + "," + contact.Name + "," + contact.Mail + "," +
                           DateTime.Now.ToString("dd-MM-yyyy");
                sw.Write(line);
            }
        }

        public long GetLastSerialNumber()
        {
            var reverseStream = new ReverseLineReader(_config["INVENTORY_PATH"]);
            var last = long.Parse(reverseStream.Take(1).First().Split(",")[0]);
            reverseStream.Close();
            return last;
        }
    }
}