using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC06_ConfigBasedApplication
{
    public class DatabaseConfig
    {
        public string Provider { get; set; } = "sqlite";
        public string ConnectionString { get; set; } = "filename";
        public int MaxConnections { get; set; } = 10;
    }
}
