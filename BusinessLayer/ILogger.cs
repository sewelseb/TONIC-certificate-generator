using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogError(string error);
    }
}
