using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Domain.Settings
{
    public class EventHubSettings
    {
        public string Hosts { get; set; }

        public const string EventHubSettingsName = nameof(EventHubSettings);
    }
}
