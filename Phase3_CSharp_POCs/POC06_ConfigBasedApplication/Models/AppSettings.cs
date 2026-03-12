using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC06_ConfigBasedApplication
{
    public class AppSettings
    {
        public string ApplicationName { get; set; }  = "firstName";
        public string Version { get; set; }  = "10";
        public string Environment { get; set; } = "environment"; 

        

       //public AppSettings() 
       // {
       //     ApplicationName = "firstTaskManager";
       //     Version = "1.0.0";
       //     Environment = "environment";
       // }

    }
}
