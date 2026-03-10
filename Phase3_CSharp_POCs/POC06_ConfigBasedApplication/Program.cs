using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace POC06_ConfigBasedApplication
{

    public class AppSettings
    {
        public string ApplicationName { get; set; }
        public int Version { get; set; }
        public string Environment { get ; set; }
    }

    public class DatabaseConfig
    {
        public string Provider {  get; set; }
        public string ConnectionString { get; set; }
        public int MaxConnections {  get; set; }
    }

    public class FeatureFlags
    {
        public bool EnableLogging {  get; set; }
        public bool EnableNotifications { get; set; }
        public bool EnableAutoSave { get; set; }
    }

    public class UserPreferences
    {
        public string Theme { get; set; }
        public string Language { get; set; }
        public DateTime DateFormat {  get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string configFilePath = "config.json"; 
            
            var defaultData1 = new AppSettings();

            defaultData1.ApplicationName = "user";
            defaultData1.Version = 1;
            defaultData1.Environment = "task";                                                          // 1

            var defaultData2 = new DatabaseConfig();

            defaultData2.Provider = "SQLite";
            defaultData2.ConnectionString = "Data Source=tasks.db";
            defaultData2.MaxConnections = 10;                                                           // 2

            var defaultData3 = new FeatureFlags();

            defaultData3.EnableLogging = true;  
            defaultData3.EnableNotifications = true;
            defaultData3.EnableAutoSave = true;                                                         // 3

            var defaultData4 = new UserPreferences();

            defaultData4.Theme = "Light";
            defaultData4.Language = "English";
            //defaultData4.DateFormat = DateTime.Now; 
            defaultData4.DateFormat = new DateTime(2000, 12, 12);                                       // 4

            Console.WriteLine($"{defaultData1.ApplicationName},{defaultData1.Version}, {defaultData1.Environment} ");
            Console.WriteLine($"{defaultData2.Provider}, {defaultData2.ConnectionString}, {defaultData2.MaxConnections} ");
            Console.WriteLine($"{defaultData3.EnableLogging}, {defaultData3.EnableNotifications}, {defaultData3.EnableAutoSave}");
            Console.WriteLine($"{defaultData4.Theme}, {defaultData4.Language}, {defaultData4.DateFormat}");

            var defaultDataCombine = new { d1 = defaultData1, d2 = defaultData2, d3 = defaultData3, d4 = defaultData4 };
            //Console.ReadLine();            

            string jsonStringg = JsonConvert.SerializeObject(defaultDataCombine, Formatting.Indented);
            File.WriteAllText(configFilePath, jsonStringg);

            string jsonFile = File.ReadAllText(configFilePath);

            var jsonString = JsonConvert.DeserializeObject(jsonFile);

            Console.WriteLine("this is the start");
            Console.WriteLine($"{jsonString} here");



            Console.ReadKey();

        }
    }
}
