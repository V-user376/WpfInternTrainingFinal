using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC06_ConfigBasedApplication
{
    internal class CombinedData
    {
        public AppSettings appSettings;
        public DatabaseConfig databaseConfig;
        public FeatureFlags featureFlags;
        public UserPreferences userPreferences;
    }
}
