using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC06_ConfigBasedApplication
{
    public class DatabaseConfig
    {
        public string Provider { get; set; }
        public string ConnectionString { get; set; }
        public int MaxConnections { get; set; }
    }
}
