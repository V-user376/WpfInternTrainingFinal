using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace JSON_FILE_OPERATIONS
{
    public class Contact
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$|^\d{3}-\d{3}-\d{4}$|^\d{3} \d{3} \d{4}$",ErrorMessage = "Phone number must be 10 digits.")]

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsFavorite { get; set; }

        //public Contact(string n, string p, string e, string a, string c, bool i )
        //{
        //    Name = n; PhoneNumber = p; Email = e; Address = a; Category = c; IsFavorite = i;
        //}                                                                                                 // parameter constructor 
        
    }

    public class ContactBook
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public DateTime LastModified { get; set; }
        public int TotalContacts { get; set; }
    }

    internal class Program
    {
        static string filePath = "contact.json";
        static void Main(string[] args)
        {
            
            ContactBook contactbook;

            bool t = false;
            //ContactBook contactbook = new ContactBook();

            if (File.Exists(filePath))
            {
                string readFile = File.ReadAllText(filePath);
                contactbook = string.IsNullOrWhiteSpace(readFile)? new ContactBook() : JsonConvert.DeserializeObject<ContactBook>(readFile);
            }
            else
            {
                contactbook = new ContactBook();
            }                  

            while (!t)
            {

                Console.WriteLine("Select operation (0-9): ");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Add New Contact");
                Console.WriteLine("2. View All Contact");
                Console.WriteLine("3. Search Contact");
                Console.WriteLine("4. Edit Contact");
                Console.WriteLine("5. Delete Contact");
                Console.WriteLine("6. Filter by Category");
                Console.WriteLine("7. Manage Favorites");
                Console.WriteLine("8. View Statistics");
                Console.WriteLine("9. Backup & Restore");

                string userChoice = Console.ReadLine();

                //string selectNotificationType = Console.ReadLine();
                switch (userChoice)
                {
                    case "0":
                        {
                            t = true;
                        }
                        break;

                    case "1":
                        {
                            Contact c = new Contact();
                            //c.ID = Guid.NewGuid();
                            Console.Write("Name: ");
                            string name = Console.ReadLine();


                            Console.Write("Phone Number: ");
                            string contact = Console.ReadLine();
                            Regex regex = new Regex(@"^[0-9]{10}$");
                            if (regex.IsMatch(contact))
                            {
                                Console.WriteLine("Valid phone number");
                            }
                            else
                            {
                                Console.WriteLine("Invalid");
                            }                                                                                // validation of phone 
                                                                                                             //var condition 

                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            //Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                            //Match match = reg.Match(name);
                            //if (match.Success)
                            //{
                            //    Console.WriteLine("Valid");
                            //}
                            //else
                            //{
                            //    Console.WriteLine("Enter a valid mail\n");
                            //    continue;                            
                            //}                                                                                 validatoin of mail

                            Console.Write("Address: ");
                            string address = Console.ReadLine();
                            //DateTime currentDateTime = DateTime.Now;

                            Console.Write("Select Category: ");
                            Console.Write("\n1. Family\n2. Friends\n3. Work\n4. Other\n Select (1-4) ");
                            string categoryNumber = Console.ReadLine();
                            string category = "";

                            switch (categoryNumber)
                            {
                                case "1":
                                    {
                                        category = "Family";
                                    }
                                    break;
                                case "2":
                                    {
                                        category = "Friends";
                                    }
                                    break;
                                case "3":
                                    {
                                        category = "Work";
                                    }
                                    break;
                                case "4":
                                    {
                                        category = "Other";
                                    }
                                    break;
                            }

                            Console.Write("Mark as Favorite? (y/n) ");
                            bool isFavorite = true;
                            string favorite = Console.ReadLine();
                            switch (favorite)
                            {
                                case "y":
                                    {
                                        isFavorite = true;
                                    }
                                    break;
                                case "n":
                                    {
                                        isFavorite = false;
                                    }
                                    break;
                            }

                            Console.WriteLine("Contact added successfully ");

                            Console.WriteLine($",{c.ID},{name},{contact},{email},{address},{category},{isFavorite}");


                            Contact cc = new Contact
                            {
                                ID = Guid.NewGuid(),
                                Name = name,
                                PhoneNumber = contact,
                                Email = email,
                                Address = address,
                                Category = category,
                                IsFavorite = isFavorite,
                                DateAdded = DateTime.Now
                            };

                            contactbook.Contacts.Add(cc);
                            contactbook.LastModified = DateTime.Now;
                            contactbook.TotalContacts = contactbook.Contacts.Count;


                            //contactbook.Contacts.Add(new Contact() { ID = c.ID, Name = name, PhoneNumber = contact, Email = email, Address = address, Category = category, IsFavorite = isFavorite, DateAdded = DateTime.Now });

                            using (StreamWriter file = File.CreateText(filePath))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Formatting = Formatting.Indented;
                                serializer.Serialize(file, contactbook);
                            }


                            //SaveDataToJSON();

                        }
                        break;

                    case "2":
                        {
                            string readFile = File.ReadAllText(filePath);
                            contactbook = string.IsNullOrWhiteSpace(readFile) ? new ContactBook() : JsonConvert.DeserializeObject<ContactBook>(readFile);



                            if (contactbook.Contacts != null)
                            {
                                foreach (var c in contactbook.Contacts)
                                {
                                    //Console.WriteLine($"ID: {c.ID}, \nName: {c.Name}, \nPhoneNumber: {c.PhoneNumber}, \nEmail: {c.Email}, \nAddress: {c.Address}, \nCategory: {c.Category}, \nIsFavorite: {c.IsFavorite}, \nDateTime: {c.DateAdded}\n");
                                    Console.WriteLine($"\n ID: {c.ID} \n Name: {c.Name} \nContact: {c.PhoneNumber}\n Email: {c.Email} Address: {c.Address} Category: {c.Category} Favorites: {c.IsFavorite} DateAdded: {c.DateAdded}");

                                    //foreach(var i in contactBook.)
                                    //{

                                    //}

                                }
                            }
                            else
                            {
                                Console.WriteLine("No Data Found");
                            }
                        }
                        break;

                    case "3":
                        {
                            Console.WriteLine("Search Contact by Name");
                            string findName = Console.ReadLine();
                            foreach (var c in contactbook.Contacts)
                            {
                                if (c.Name == findName)
                                {
                                    Console.WriteLine($"{c.ID}{c.Name}{c.PhoneNumber}{c.Email}{c.Address}{c.IsFavorite}{c.DateAdded}");
                                }
                            }

                        }
                        break;

                    case "4":
                        //{
                        //    Guid numberToFind = Guid.Parse(Console.ReadLine());
                        //    Contact foundContact = contactbook.FirstOrDefault(),.(s => s.ID == numberToFind);

                        //    Contact ffound = contactbook.LastModified





                        //    if (foundContact != null)
                        //    {
                        //        Console.WriteLine($"Details for {numberToFind}:");
                        //        Console.WriteLine($"Name {foundContact.Name} ");
                        //        Console.WriteLine($"PhoneNumber {foundContact.PhoneNumber} ");
                        //        Console.WriteLine($"Email {foundContact.Email} ");
                        //        Console.WriteLine($"Address {foundContact.Address} ");
                        //        Console.WriteLine($"Category {foundContact.Category} ");
                        //        Console.WriteLine($"DateAdded {foundContact.DateAdded} ");
                        //        Console.WriteLine($"IsFavorite {foundContact.IsFavorite} ");


                        //        Console.WriteLine("Enter the field you want to edit");
                        //        string fieldToEdit = Console.ReadLine();
                        //        string newValue;

                        //        switch (fieldToEdit.ToLower())
                        //        {
                        //            case "name":
                        //                newValue = Console.ReadLine();
                        //                foundContact.Name = newValue;
                        //                Console.WriteLine($"Your new name is {foundContact.Name}");

                        //                break;

                        //        }
                        //    }
                        //}
                        //break;

                    case "5":

                        //{
                        //    Console.WriteLine("\nEnter the name of student to delete details");
                        //    string nameToFind = Console.ReadLine();

                        //    Contact deleteContact = contacts.FirstOrDefault(s => s.Name == nameToFind);
                        //    contacts.RemoveAt(contacts.IndexOf(deleteContact));

                        //    Console.WriteLine($"This {nameToFind} contact is now deleted from record");

                        //}
                        //break;


                    case "6":
                        {


                            Console.WriteLine("Enter 1 for Family, 2 for Friends, 3 for Work, 4 for Other");
                            string categoryName = Console.ReadLine();
                            string category = "";

                            switch (categoryName)
                            {
                                case "1":
                                    category = "Family";
                                    break;


                                case "2":
                                    category = "Friends";
                                    break;

                                case "3":
                                    category = "Work";
                                    break;

                                case "4":
                                    category = "Other";
                                    break;
                            }


                            bool categoryBool = false;
                            foreach (var c in contactbook.Contacts)
                            {
                                if (c.Category == category)
                                {
                                    Console.WriteLine($"\n ID: {c.ID} \n Name: {c.Name} \nContact: {c.PhoneNumber}\n Email: {c.Email}\n Address: {c.Address}\n Category: {c.Category}\n Favorites: {c.IsFavorite}\n DateAdded: {c.DateAdded}\n");

                                    categoryBool = true;
                                }
                            }
                        }
                        break;
                    case "7":
                        {
                            Console.WriteLine("Enter 1 for Favorites, 2 for Others");
                            string categoryName = Console.ReadLine();
                            string favorites = "";

                            switch (categoryName)
                            {
                                case "1":
                                    favorites = "true";
                                    break;


                                case "2":
                                    favorites = "false";
                                    break;

                            }

                            bool categoryBool = false;
                            foreach (var c in contactbook.Contacts)
                            {
                                if (c.IsFavorite = bool.Parse(favorites))
                                {
                                    Console.WriteLine($"\n ID: {c.ID} \n Name: {c.Name} \nContact: {c.PhoneNumber}\n Email: {c.Email}\n Address: {c.Address}\n Category: {c.Category}\n Favorites: {c.IsFavorite}\n DateAdded: {c.DateAdded}\n");

                                    categoryBool = true;
                                }
                            }
                        }
                        break;
                    case "8":
                        {
                        }
                        break;
                    case "9":
                        {

                            Console.WriteLine("0 for Backup");
                            Console.WriteLine("1 for List all backup");
                            Console.WriteLine("2 Keep last five backup");
                            Console.WriteLine("3 Restore backup");


                            string tasks = Console.ReadLine();
                            switch (tasks)
                            {
                                case "0":
                                    {
                                        CreateBackup(filePath);
                                    }
                                    break;

                                case "1":
                                    {
                                        ListAllBackup();
                                    }
                                    break;

                                case "2":
                                    {
                                        KeepLastFiveBackup();
                                    }
                                    break;

                                case "3":
                                    {
                                        RestoreBackup();
                                    }
                                    break;
                            }
                            break;

                        }
                
                }
            }                      
        }

        static string backupFolder = "Backup";
        public static void CreateBackup(string filePath)
        {
            try
            {
                if(!Directory.Exists(backupFolder))
                {
                    Directory.CreateDirectory(backupFolder);
                }
                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFileName = $"backup_{timeStamp}.json";
                string backupPath = Path.Combine(backupFolder, backupFileName);

                File.Copy(filePath, backupPath);

                Console.WriteLine($"{backupFileName} file created.");     

            }

            catch(Exception ex) 
            {
                Console.WriteLine($"Backup Failed {ex.Message}");
            }
        }

        public static void ListAllBackup()
        {
            string[] files = Directory.GetFiles(backupFolder, "*.json");

            if (!Directory.Exists(backupFolder))
            {
                Console.WriteLine("No backup available");
            }

            if (files.Length != 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{Path.GetFileName(files[i])}");
                }
            }
        }
        public static void KeepLastFiveBackup()
        {
            string[] files = Directory.GetFiles(backupFolder, "*.json").OrderByDescending(x => x.Last()).ToArray();

            if(files.Length > 5)
            {
                for(int i = 5; i < files.Length; i++)
                {
                    File.Delete(files[i]);
                }

            }
        }

        public static void RestoreBackup()
        {

            ListAllBackup();
            string[] files = Directory.GetFiles(backupFolder, "*.json");

            Console.WriteLine("Give file number");
            int selectFile = Convert.ToInt32(Console.ReadLine());

            if (selectFile < 1 || selectFile > files.Length)
            {
                Console.WriteLine("Wrong Input");
            }

           

            var sourceFile = File.ReadAllText(filePath);

            var destinationFile = File.ReadAllText(files[selectFile]);

            //List<ContactBook> contactList = JsonSerializer.Deserialize<List<ContactBook>>(sourceFile) ?? new List<ContactBook>();
            ContactBook currentData = JsonConvert.DeserializeObject<ContactBook>(sourceFile) ?? new ContactBook();

            ContactBook backupData = JsonConvert.DeserializeObject<ContactBook>(destinationFile) ?? new ContactBook();


            currentData.Contacts.AddRange(backupData.Contacts);

            currentData.TotalContacts = currentData.Contacts.Count;

            currentData.LastModified = DateTime.Now;



            string mergedData = JsonConvert.SerializeObject(currentData, Formatting.Indented);
            File.WriteAllText(filePath, mergedData);
            




        }
    }
}
