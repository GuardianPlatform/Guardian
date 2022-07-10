using System;
using System.Collections.Generic;
using System.Text;
using Guardian.Logging.Api;

namespace Guardian.Logging.Contract
{
    public class Log
    {
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
