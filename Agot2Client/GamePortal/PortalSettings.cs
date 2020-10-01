using Agot2Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace GamePortal
{
    public class PortalSettings
    {
        public ConcurrentDictionary<string, GPUser> GamePortal { get; set; }

        public PortalSettings()
        {
            GamePortal = new ConcurrentDictionary<string, GPUser>();
        }
    }
}
