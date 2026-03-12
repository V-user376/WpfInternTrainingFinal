using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC06_ConfigBasedApplication
{
    public class UserPreferences
    {
        public string Theme { get; set; } = "Light";
        public string Language { get; set; } = "English";
        public string DateFormat { get; set; } = "currentDate";
    }
}   
