using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace POC06_ConfigBasedApplication
{

    internal class Program
    {
        


        static void Main(string[] args)
        {
            string configFilePath = "config.json";                        

            string jsonFile = File.ReadAllText(configFilePath);
           
            CombinedData cd = JsonConvert.DeserializeObject<CombinedData>(jsonFile);
            
            Console.WriteLine(jsonFile);

                      

            Console.WriteLine(cd.appSettings.ApplicationName);

            Console.WriteLine("Enter the field to edit");

            string userInput = Console.ReadLine();

            var newValue = Console.ReadLine();

            switch (userInput)
            {
                case "applicationName":
                    cd.appSettings.ApplicationName = newValue;
                    break;

                case "version":
                    cd.appSettings.Version = newValue;
                    break;

                case "environment":
                    cd.appSettings.Environment = newValue;
                    break;

                case "provider":
                    cd.databaseConfig.Provider = newValue;
                    break;

                case "connectionString":
                    cd.databaseConfig.ConnectionString = newValue;
                    break;

                case "maxConnections":
                    cd.databaseConfig.MaxConnections = Convert.ToInt32(newValue);
                    break;

                case "enableLogging":
                    cd.featureFlags.EnableLogging = Convert.ToBoolean(newValue);
                    break;

                case "enableNotification":
                    cd.featureFlags.EnableNotifications = Convert.ToBoolean(newValue);
                    break;

                case "enableAutoSave":
                    cd.featureFlags.EnableAutoSave = Convert.ToBoolean(newValue);
                    break;

                case "theme": 
                    cd.userPreferences.Theme = newValue;
                    break;

                case "language":
                    cd.userPreferences.Language = newValue;
                    break;

                case "dateFormat":
                    cd.userPreferences.DateFormat = Convert.ToDateTime(newValue);
                    break;
            }
           
            


            string updatedJson = JsonConvert.SerializeObject(cd,Formatting.Indented);
            File.WriteAllText(configFilePath, updatedJson);


            Console.ReadKey();

        }
    }
}
