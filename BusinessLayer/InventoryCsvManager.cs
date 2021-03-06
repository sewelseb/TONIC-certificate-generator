﻿using System;
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

        public Contact AddSerialNumber(Contact contact)
        {
            contact.SerialNumber = GetLastSerialNumber() + 1;
            using (var streamWriter = new StreamWriter(_config["INVENTORY_PATH"], true))
            {
                var line = $"\n{contact.SerialNumber},{DateTime.Now.ToString("dd-MM-yyyy")},{contact.Name},{contact.Mail},{_config["CONFERENCE_NAME"]},{_config["CONFERENCE_DATE"]}";
                streamWriter.Write(line);
            }

            return contact;
        }

        public long GetLastSerialNumber()
        {
            var reverseStream = new ReverseLineReader(_config["INVENTORY_PATH"]);
            var last = ReturnSerialNumberFromLastLine(reverseStream);
            reverseStream.Close();
            return last;
        }

        private static long ReturnSerialNumberFromLastLine(ReverseLineReader reverseStream)
        {
            return long.Parse(reverseStream.Take(1).First().Split(",")[0]);
        }
    }
}