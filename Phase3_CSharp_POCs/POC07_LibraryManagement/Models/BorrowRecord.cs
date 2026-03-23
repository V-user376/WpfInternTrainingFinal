using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Web;

namespace POC07_LibraryManagement.Models
{
    public class BorrowRecord
    {
        //  Properties
        public int RecordId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int Fine { get; set; }

        //  Create BorrowRecord Table
        public static void CreateTable(SQLiteConnection connection)
        {
            string query = @"CREATE TABLE IF NOT EXISTS BorrowRecord(
                RecordId INTEGER PRIMARY KEY AUTOINCREMENT,
                BookId INTEGER,
                MemberId INTEGER, 
                BorrowDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                Duedate DATETIME NOT NULL, 
                Returndate DATETIME NULL,
                Fine INTEGER DEFAULT 0,
                FOREIGN KEY(BookId) REFERENCES Book(BookId),
                FOREIGN KEY(MemberId) REFERENCES Members(MemberId)
            )";

            using (var cmd = new SQLiteCommand(query, connection))
                cmd.ExecuteNonQuery();
        }

        //  Add Borrow Record
        public void Add(SQLiteConnection connection)
        {
            // BookId
            while (true)
            {
                Console.WriteLine("Enter Book ID:");
                if (int.TryParse(Console.ReadLine(), out int bid))
                {
                    BookId = bid;
                    break;
                }
                Console.WriteLine("Invalid Book ID");
            }

            // MemberId
            while (true)
            {
                Console.WriteLine("Enter Member ID:");
                if (int.TryParse(Console.ReadLine(), out int mid))
                {
                    MemberId = mid;
                    break;
                }
                Console.WriteLine("Invalid Member ID");
            }

            // Borrow Date
            Console.WriteLine("Enter Borrow Date (yyyy-MM-dd) or leave blank for today:");
            string borrowInput = Console.ReadLine();
            if (!DateTime.TryParse(borrowInput, out DateTime borrow))
                borrow = DateTime.Now;
            BorrowDate = borrow;

            // Due Date
            while (true)
            {
                Console.WriteLine("Enter Due Date (yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime due))
                {
                    DueDate = due;
                    break;
                }
                Console.WriteLine("Invalid date");
            }

            // Return Date
            while (true)
            {
                Console.WriteLine("Enter Return Date (yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime returnDate))
                {
                    ReturnDate = returnDate;
                    break;
                }
                Console.WriteLine("Invalid date");
            }



            //Console.WriteLine("Enter Borrow Date (yyyy-MM-dd) or leave blank for today:");
            //string returnInput = Console.ReadLine();
            //if (!DateTime.TryParse(returnInput, out DateTime returnToday))
            //    returnToday = DateTime.Now;
            //ReturnDate = returnToday;


            Fine = 0;

            string query = @"INSERT INTO BorrowRecord
                (BookId, MemberId, BorrowDate, Duedate, Returndate, Fine)
                VALUES (@bookId, @memberId, @borrowDate, @dueDate, @returnDate, @fine)";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@bookId", BookId);
                cmd.Parameters.AddWithValue("@memberId", MemberId);
                cmd.Parameters.AddWithValue("@borrowDate", BorrowDate);
                cmd.Parameters.AddWithValue("@dueDate", DueDate);
                cmd.Parameters.AddWithValue("@returnDate", ReturnDate);
                cmd.Parameters.AddWithValue("@fine", Fine);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Borrow record added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding borrow record: " + ex.Message);
                }
            }
        }

        //  Optional: List all borrow records
        public static void ShowBorrowRecords(SQLiteConnection connection)
        {
            string sqlQuery = "SELECT * FROM BorrowRecord";

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


        public static void FineCalculation(SQLiteConnection connection)
        {
            string sqlQuery = "SELECT * FROM BorrowRecord";
            var record = new List<(int id, int fine)>();

            using(var command = new SQLiteCommand(sqlQuery, connection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string startDate = reader["Duedate"].ToString();
                    string endDate = reader["Returndate"].ToString();
                    int id = Convert.ToInt32(reader["RecordId"]);

                    DateTime startDates = DateTime.Parse(startDate);
                    DateTime endDates = DateTime.Parse(endDate);

                    int delayDays = (endDates - startDates).Days;

                    //Console.WriteLine("this is the total delay in days");
                    //Console.WriteLine(delayDays);

                    int totalFine = delayDays * 1;
                    Console.WriteLine($"Total fine is {totalFine}");
                    record.Add((id, totalFine));

                }                                   
            }



            foreach(var r in record)
            {
                string updateQuery = "UPDATE BorrowRecord SET Fine = @fine WHERE RecordId = @id";

                using (var cmd = new SQLiteCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@fine", r.fine);
                    cmd.Parameters.AddWithValue("@id", r.id);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }


        

    }
}
