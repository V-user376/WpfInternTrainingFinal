using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using POC07_LibraryManagement.Models;



namespace POC07_LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=library.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                connection.Open();
                using (var cmd = new SQLiteCommand("PRAGMA foreign_keys = ON", connection))
                {
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Connection established");


                Books.CreateTables(connection);
                Member.CreateTable(connection);
                BorrowRecord.CreateTable(connection);

                Books book = new Books();
                //book.Add(connection);

                Member member = new Member();
                //member.Add(connection);

                BorrowRecord borrowRecord = new BorrowRecord();
                //borrowRecord.Add(connection);
                
                //Books.ShowBooks(connection);

                //Member.ShowMembers(connection);

                //BorrowRecord.ShowBorrowRecords(connection);

                //Books.UpdateBooks(connection);

                //Books.DeleteBook(connection);

                //BorrowRecord.FineCalculation(connection);

                //Books.TotalBooks(connection);
                //Member.TotalMembers(connection);

                //Reports.CurrentlyBorrowed(connection);

                //Reports.OverdueBooks(connection);
                //Reports.MostBorrowedBooks(connection);
                //Reports.MostActiveMember(connection);

                //Reports.BooksNeverBorrowed(connection);
                //Reports.CategoryWise(connection);



                Console.ReadKey();
                Console.WriteLine();


                bool boolValue1 = true;
                while (boolValue1)
                {

                    Console.WriteLine("1. Book Management");
                    Console.WriteLine("2. Member Management");
                    Console.WriteLine("3. FineCalculation");
                    Console.WriteLine("4. Report & Statistics");                    
                    Console.WriteLine("5. Borrow/Return System");
                    

                    string inputNumber = Console.ReadLine();

                    switch (inputNumber)
                    {
                        case "1":

                            bool boolValue2 = true;
                            while (boolValue2)
                            {

                                Console.WriteLine("1 to Add New Book");
                                Console.WriteLine("2 to View All Books");
                                Console.WriteLine("3 to Search Books");
                                Console.WriteLine("4 to Update Book");
                                Console.WriteLine("5 to Delete Book");
                                Console.WriteLine("6 to Exit");

                                string inputNumbers = Console.ReadLine();

                                switch (inputNumbers)
                                {
                                    case "1":
                                        {
                                            book.Add(connection);
                                        }

                                        break;

                                    case "2":
                                        {
                                            Books.ShowBooks(connection);
                                        }
                                        break;

                                    case "3":
                                        {

                                        }

                                        break;


                                    case "4":
                                        {
                                            Books.UpdateBooks(connection);
                                        }

                                        break;

                                    case "5":
                                        {
                                            Books.DeleteBook(connection);
                                        }


                                        break;

                                    case "6":
                                        System.Environment.Exit(0);
                                        Console.WriteLine("Exit");
                                        break;
                                }
                            }
                            break;

                        case "2":

                            bool boolValue3 = true;
                            while (boolValue3)
                            {

                                Console.WriteLine("1 Add New Member");
                                Console.WriteLine("2 View All Members");
                                Console.WriteLine("3 Search Members");
                                Console.WriteLine("4 Update Member Data");
                                Console.WriteLine("5 Delete Member Data");
                                Console.WriteLine("6 to Exit");

                                string inputNumbers = Console.ReadLine();

                                switch (inputNumbers)
                                {
                                    case "1":
                                        {
                                            member.Add(connection);
                                        }

                                        break;

                                    case "2":
                                        {
                                            Member.ShowMembers(connection);
                                        }

                                        break;

                                    case "3":
                                        {
                                            
                                        }

                                        break;


                                    case "4":
                                        {
                                            Member.UpdateMembers(connection);
                                        }

                                        break;

                                    case "5":
                                        {
                                            Member.DeleteMember(connection);
                                        }
                                        break;

                                    case "6":
                                        System.Environment.Exit(0);
                                        Console.WriteLine("Exit");
                                        break;
                                }
                            }
                            break;





                        case "3":

                            {
                                BorrowRecord.FineCalculation(connection);
                            }

                            break;


                        case "4":


                            break;

                        case "5":

                            bool boolValue4 = true;
                            while (boolValue4)
                            {

                                Console.WriteLine("1 Issue book");
                                Console.WriteLine("2 Return book");
                                Console.WriteLine("3 Currently borrowed book");
                                Console.WriteLine("4 Overdue books");
                                Console.WriteLine("5 Most borrowed books");
                                Console.WriteLine("6 Most active members");
                                Console.WriteLine("7 Books never borrowed");
                                Console.WriteLine("8 Categorywise book distribution");


                                string inputNumbers = Console.ReadLine();

                                switch (inputNumbers)
                                {
                                    case "1":
                                        {
                                            Reports.IssueBook(connection);
                                        }

                                        break;

                                    case "2":
                                        {
                                            Reports.ReturnBook(connection);
                                        }
                                        break;

                                    case "3":
                                        {
                                            Reports.CurrentlyBorrowed(connection);
                                        }

                                        break;


                                    case "4":
                                        {
                                            Reports.IdentifyOverdueBooks(connection);
                                        }

                                        break;
                                   
                                }
                            }                            

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
}

        
        





















//bool boolValue1 = true;
//while (boolValue1)
//{

//    Console.WriteLine("1 to Add New Book");
//    Console.WriteLine("2 to View All Books");
//    Console.WriteLine("3 to Search Books");
//    Console.WriteLine("4 to Update Book");
//    Console.WriteLine("5 to Delete Book");
//    Console.WriteLine("6 to Exit");

//    string inputNumber = Console.ReadLine();

//    switch (inputNumber)
//    {
//        case "1":


//            break;

//        case "2":


//            break;

//        case "3":

//            break;


//        case "4":


//            break;

//        case "5":

//            break;

//        case "6":
//            System.Environment.Exit(0);
//            Console.WriteLine("Exit");
//            break;