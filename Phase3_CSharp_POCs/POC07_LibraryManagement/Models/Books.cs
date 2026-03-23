using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.ComponentModel.Design;
using System.Runtime.Remoting.Services;

namespace POC07_LibraryManagement.Models
{
    internal class Books
    {
        
        public int BookId { get; set; }      
        public string ISBN { get; set; }        
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int PublishYear { get; set; }
        public int TotalCopies { get; set; }
        public int Available { get; set; }
        public DateTime DateAdded { get; set; }


        public static void CreateTables(SQLiteConnection connection)
        {
            string createBookTable = @"CREATE TABLE IF NOT EXISTS Book(
                BookId INTEGER PRIMARY KEY AUTOINCREMENT,                        
                ISBN TEXT UNIQUE NOT NULL,
                Title TEXT NOT NULL,
                Author TEXT NOT NULL, 
                Category TEXT, 
                PublishYear INTEGER,
                TotalCopies INTEGER DEFAULT 1,
                Available INTEGER DEFAULT 1,
                DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
            )";

            using (SQLiteCommand cmd = new SQLiteCommand(createBookTable, connection))


                cmd.ExecuteNonQuery();
        }

            public void Add(SQLiteConnection connection)
            {
                Console.WriteLine("Enter ISBN:");
                ISBN = Console.ReadLine();

                if (ISBN.Length != 3 && ISBN.Length != 5)
                {
                    Console.WriteLine("ISBN should be 3 or 5 characters");
                    return;
                }

                Console.WriteLine("Enter Title:");
                Title = Console.ReadLine();

                Console.WriteLine("Enter Author:");
                Author = Console.ReadLine();

                Console.WriteLine("Enter Category:"); 
                Category = Console.ReadLine();



                int year;
                while (true)
                {
                    Console.WriteLine("Enter Publish Year:");
                    if (int.TryParse(Console.ReadLine(), out year))
                    {
                        PublishYear = year;
                        break;
                    }
                    Console.WriteLine("Invalid input. Enter a valid year.");
                }

                int total;
                while (true)
                {
                    Console.WriteLine("Enter Total Copies:");
                    if (int.TryParse(Console.ReadLine(), out total))
                    {
                        TotalCopies = total;
                        break;
                    }
                    Console.WriteLine("Invalid input. Enter a valid number.");
                }

                int available;
                while (true)
                {
                    Console.WriteLine("Enter Available Copies:");
                    if (int.TryParse(Console.ReadLine(), out available))
                    {
                        while (available > total)
                        {
                        Console.WriteLine("Enter value of available again");

                        int.TryParse(Console.ReadLine(), out available);
                        }

                        
                        Available = available;
                        break;
                    }
                    Console.WriteLine("Invalid input. Enter a valid number.");
                }


                Console.WriteLine("Enter Date or leave blank for today:");
                string dateInput = Console.ReadLine();
                if (!DateTime.TryParse(dateInput, out DateTime dt))
                    dt = DateTime.Now;
                    DateAdded = dt;

                string query = @"INSERT INTO Book
                    (ISBN, Title, Author, Category, PublishYear, TotalCopies, Available, DateAdded)
                    VALUES (@isbn, @title, @author, @category, 
                            @publishYear, @totalCopies, @available, @dateAdded)";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@isbn", ISBN);
                    cmd.Parameters.AddWithValue("@title", Title);
                    cmd.Parameters.AddWithValue("@author", Author);
                    cmd.Parameters.AddWithValue("@category", Category);
                    cmd.Parameters.AddWithValue("@publishYear", PublishYear);
                    cmd.Parameters.AddWithValue("@totalCopies", TotalCopies);
                    cmd.Parameters.AddWithValue("@available", Available);
                    cmd.Parameters.AddWithValue("@dateAdded", DateAdded);

                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Book added successfully!");
            }


        public static void ShowBooks(SQLiteConnection connection)
        {
            string sqlQuery = "SELECT * FROM Book";

            using (var command = new SQLiteCommand(sqlQuery, connection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                // Print column headers
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader.GetName(i),-40}");
                }
                Console.WriteLine("\n----------------------------------------");

                // Read and print data row by row
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // Access column values by index or name
                        Console.Write($"{reader[i].ToString(),-20}");
                    }
                    Console.WriteLine();
                }
            }
        }

        //public static void UpdateBooks(SQLiteConnection connection)
        //{
        //    int selectBookId = int.Parse(Console.ReadLine());
        //    string newTotalValue = Console.ReadLine();





        //    string query = $"UPDATE BOOK SET TOTALCOPIES = @Total WHERE ID = {selectBookId}";






        //}


        public static void UpdateBooks(SQLiteConnection connection)
        {
            // Take BookId
            Console.WriteLine("Enter Book ID to update:");
            int selectBookId;
            while (!int.TryParse(Console.ReadLine(), out selectBookId))
            {
                Console.WriteLine("Invalid ID. Enter a valid number:");
            }

            // Take new TotalCopies
            Console.WriteLine("Enter new Total Copies:");
            int newTotalValue;
            while (!int.TryParse(Console.ReadLine(), out newTotalValue))
            {
                Console.WriteLine("Invalid number. Enter again:");
            }

            // Update query
            string query = "UPDATE Book SET TotalCopies = @Total WHERE BookId = @Id";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Total", newTotalValue);
                cmd.Parameters.AddWithValue("@Id", selectBookId);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Book updated successfully!");
                    else
                        Console.WriteLine("No book found with given ID.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating book: " + ex.Message);
                }
            }
        }

        public static void DeleteBook(SQLiteConnection connection)
        {
            Console.WriteLine("Enter Book Id");
            int bookId = int.Parse(Console.ReadLine());

            string checkQuery = "SELECT COUNT(*) FROM Book WHERE BookId = @bookId";

            using (var cmd = new SQLiteCommand(checkQuery, connection))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);

                long count = (long)cmd.ExecuteScalar();

                if (count < 0)
                {
                    Console.WriteLine("Cannot delete book");
                    return;
                }
            }

            string deleteQuery = "DELETE FROM Book WHERE BookId = @bookId";

            using (var cmd = new SQLiteCommand(deleteQuery, connection))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Deleted book");

            }

        }

        public static void TotalBooks(SQLiteConnection connection)
        {
            string query = "SELECT COUNT (*) FROM Book";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"total books {count}");
            }
        }










            
        //public static void TrackAvailablity(SQLiteConnection connection)
        //{
        //    string query = "SELECT * Book";

        //    using(SQLiteCommand cmd = new SQLiteCommand(query, connection))
        //    using(SQLiteDataReader reader = cmd.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            int available = (reader["Avail"]
        //        }
        //    }
        //}




        // You can also add Update, Delete, GetAll methods here
    }
}

