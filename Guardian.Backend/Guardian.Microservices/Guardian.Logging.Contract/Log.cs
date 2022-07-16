using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Logging.Contract
{
    public class Log
    {
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
