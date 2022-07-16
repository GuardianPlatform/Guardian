using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Domain.Settings
{
    public class MicroservicesSettings
    {
        public string LoggingApiUrl { get; set; }

        public const string MicroservicesSettingsName = nameof(MicroservicesSettings);
    }
}
