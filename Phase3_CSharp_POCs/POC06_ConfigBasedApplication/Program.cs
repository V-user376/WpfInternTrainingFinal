using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Services;
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

            
            
            CombinedData cd = new CombinedData();


            if (File.Exists(configFilePath))
            {
                string jsonFile = File.ReadAllText(configFilePath);

                cd = JsonConvert.DeserializeObject<CombinedData>(jsonFile);
            }
            else
            {
               
                cd.appSettings = new AppSettings();
                cd.databaseConfig = new DatabaseConfig();
                cd.featureFlags = new FeatureFlags();
                cd.userPreferences = new UserPreferences();
            }

            //CombinedData cd = new CombinedData();
            //cd.appSettings = new AppSettings();
            //cd.userPreferences = new UserPreferences();

            bool boolValue1 = true;
            while (boolValue1)
            {

                Console.WriteLine("1 for View Configuration");
                Console.WriteLine("2 for Update User Preferences");
                Console.WriteLine("3 for Toggle Feature Flags");
                Console.WriteLine("4 for Test Config-Driven Behavior");
                Console.WriteLine("5 for Reload Configuration");
                Console.WriteLine("6 for Exit");

                string inputNumber = Console.ReadLine();

                switch (inputNumber)
                {
                    case "1":

                        string jsonOutput = JsonConvert.SerializeObject(cd, Formatting.Indented);
                        Console.WriteLine(jsonOutput);
                        if (!File.Exists(configFilePath))
                        {
                            File.AppendAllText(configFilePath, jsonOutput);
                        }
                        //Console.ReadKey();
                        //Console.Clear();
                        //Console.ReadKey();
                        break;

                    case "2":

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
                                
                                    int intInput = 0;
                                    bool isValid = false;
                                    while (!isValid)
                                    {
                                        newValue = Console.ReadLine();
                                        isValid = int.TryParse(newValue, out intInput);
                                        if (!isValid)
                                        {
                                            Console.WriteLine("Enter integer value");
                                        }
                                    }
                                
                                    cd.databaseConfig.MaxConnections = intInput;
                                
                                    break;

                            case "enableLogging":

                                bool boolInput;
                                bool isBool1 = false;
                                while (!isBool1)
                                {
                                    newValue = Console.ReadLine();
                                    isBool1 = bool.TryParse(newValue, out boolInput);
                                    if (!isBool1)
                                    {
                                        Console.WriteLine("Enter boolean value");
                                    }
                                }                              
                                cd.featureFlags.EnableLogging = Convert.ToBoolean(newValue);
                                break;

                            case "enableNotification":

                                bool boolInput1;
                                bool isBool2 = false;
                                while (!isBool2)
                                {
                                    newValue = Console.ReadLine();
                                    isBool2 = bool.TryParse(newValue, out boolInput1);
                                    if (!isBool2)
                                    {
                                        Console.WriteLine("Enter boolean value");
                                    }
                                }


                                cd.featureFlags.EnableNotifications = Convert.ToBoolean(newValue);
                                break;

                            case "enableAutoSave":

                                bool boolInput2;
                                bool isBool3 = false;
                                while (!isBool3)
                                {
                                    newValue = Console.ReadLine();
                                    isBool3 = bool.TryParse(newValue, out boolInput2);
                                    if (!isBool3)
                                    {
                                        Console.WriteLine("Enter boolean value");
                                    }
                                }

                                cd.featureFlags.EnableAutoSave = Convert.ToBoolean(newValue);
                                break;

                            case "theme":
                                cd.userPreferences.Theme = newValue;
                                break;

                            case "language":
                                cd.userPreferences.Language = newValue;
                                break;

                            case "dateFormat":
                                cd.userPreferences.DateFormat = newValue;
                                break;



                        }
                        Console.Clear();
                        string updatedJson = JsonConvert.SerializeObject(cd, Formatting.Indented);
                        File.WriteAllText(configFilePath, updatedJson);

                        string jsonFile = File.ReadAllText(configFilePath);
                        Console.WriteLine(jsonFile);
                        //Console.ReadKey();

                        break;


                    case "3":
                        {

                            Console.WriteLine($"EnableLogging: {cd.featureFlags.EnableLogging}");
                            Console.WriteLine($"EnableNotifications: {cd.featureFlags.EnableNotifications}");
                            Console.WriteLine($"EnableAutoSave: {cd.featureFlags.EnableAutoSave}");

                            Console.WriteLine("Enter the value from FeatureFlag");
                            string enableChoice = Console.ReadLine();

                            Console.WriteLine("Enter the boolean value to change ");
                            string boolValue = Console.ReadLine();

                            bool checkValue = false;
                            while (!checkValue)
                            {

                                switch (enableChoice)
                                {
                                    case "enableLogging":
                                        
                                        cd.featureFlags.EnableLogging = Convert.ToBoolean(boolValue);
                                        break;
                                    case "enableNotification":
                                        cd.featureFlags.EnableNotifications = Convert.ToBoolean(boolValue);
                                        break;
                                    case "enableAutoSave":
                                        cd.featureFlags.EnableAutoSave = Convert.ToBoolean(boolValue);
                                        break;
                                }
                            }
                            string updatedJson1 = JsonConvert.SerializeObject(cd, Formatting.Indented);
                            File.WriteAllText(configFilePath, updatedJson1);                        

                            break;
                        }

                        Console.WriteLine(cd.appSettings.ApplicationName);

                        Console.ReadKey();



                    case "4":

                        string enableLog = Convert.ToString(cd.featureFlags.EnableLogging);
                        string enableNotification = Convert.ToString(cd.featureFlags.EnableNotifications);
                        string enableAutoSave = Convert.ToString(cd.featureFlags.EnableAutoSave);
                        if (enableLog == "True")
                        {
                            string log = "Application Started";
                            DateTime currentTime = DateTime.Now;
                            Console.WriteLine($"{currentTime} Log: {log}");
                            if (enableAutoSave != "True")
                            {
                                Console.WriteLine("AutoSave is Disabled");
                            }
                            else
                            {
                                Console.WriteLine("AutoSave is Working");
                            }

                            Console.WriteLine($"Current theme is {cd.userPreferences.Theme}");

                        }
                        else
                        {
                            Console.WriteLine("EnableLogging is False now");
                        }

                        Console.ReadKey();
                        break;

                    case "6":                                            
                        System.Environment.Exit(0);
                        Console.WriteLine("Exit");
                        break;

                }
            }
        }       
    }
}
