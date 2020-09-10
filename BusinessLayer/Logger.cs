using Serilog;
using Serilog.Sinks.SystemConsole;
using System;


namespace BusinessLayer
{
    class Logger : ILogger
    {
        Serilog.Core.Logger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public void LogInformation(string message)
        {
            log.Information(message);
        }

        public void LogError(string message)
        {
            log.Error(message);
        }
    }
}
