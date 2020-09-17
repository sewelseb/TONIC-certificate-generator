using Microsoft.Extensions.Configuration;
using Models;
using Serilog;
using System;
using System.IO;

namespace BusinessLayer
{
    public class InventoryCsvManager : IInventoryManager
    {
        private StreamWriter _fileW;
        private StreamReader _fileR;
        private IConfigurationRoot _config;
        private ILogger _logger;

        private ReverseLineReader r;

        public InventoryCsvManager(IConfigurationRoot config, ILogger logger)
        {
            if (!File.Exists(config[""])) throw new FileNotFoundException();
            
            _config = config;
            _logger = logger;
            _fileW = new StreamWriter(config[""], true );
            _fileR = new StreamWriter(config[""], true );
        }

        public int GetLastSerialNumber()
        {
            return 0;
        }

        public void AddSerialNumber(Contact contact)
        {

        }

    }
}