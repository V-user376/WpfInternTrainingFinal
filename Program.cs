using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JSON_FILE_OPERATIONS
{
    public class Contact
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsFavorite { get; set; }
    }

    public class ContactBook
    {
        public List<Contact> Constacts { get; set; }

        public DateTime LastModified { get; set; }
        public int TotalContacts { get; set; }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            bool t = false;
            while (!t)
            {

                Console.WriteLine("Select operation (0-9): ");

                string userChoice = Console.ReadLine();

                string selectNotificationType = Console.ReadLine();
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
                            c.ID = Guid.NewGuid();
                            Console.WriteLine("Name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Phone Number");
                            string contact = Console.ReadLine();
                            Console.WriteLine("Email");
                            string email = Console.ReadLine();
                            Console.WriteLine("Address");
                            string address = Console.ReadLine();
                            Console.WriteLine("Select Category");
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

                            Console.WriteLine("Mark as Favorite?");
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
                            Console.WriteLine($",{c},{name},{contact},{email},{address},{category},{isFavorite}");

                        }
                        break;

                    case "2":
                        {
                        }
                        break;
                    case "3":
                        {
                        }
                        break;
                    case "4":
                        {
                        }
                        break;
                    case "5":
                        {
                        }
                        break;
                    case "6":
                        {
                        }
                        break;
                    case "7":
                        {
                        }
                        break;
                    case "8":
                        {
                        }
                        break;
                    case "9":
                        {
                        }
                        break;
                }
            }
        }
    }
}
