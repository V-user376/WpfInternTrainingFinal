using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC06_ConfigBasedApplication
{
    public class FeatureFlags
    {
        public bool EnableLogging { get; set; }
        public bool EnableNotifications { get; set; }
        public bool EnableAutoSave { get; set; }
    }
}
