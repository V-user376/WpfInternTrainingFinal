using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace POC07_LibraryManagement.Models
{
    //internal class Report
    //{

    //    private SQLiteConnection connection;

    //    public Report(SQLiteCommand command)
    //    {
    //        command = connection;
    //    }


    //    public void TotalBooks()
    //    {
    //        string query = "SELECT COUNT(*) FROM Book";

    //        using(var cmd = new SQLiteCommand(query, connection))
    //        {
    //            int count = Convert.ToInt32(cmd.ExecuteScalar());
    //        }
    //    }
    //}

    public static class Reports
    {
        public static void CurrentlyBorrowed(SQLiteConnection connection)
        {
            string query = "SELECT COUNT(*) FROM BorrowRecord WHERE Returndate IS NULL";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"Currently Borrowed Books {count}");
            }
        }


        public static void OverdueBooks(SQLiteConnection connection)
        {
            string query = @"SELECT br.RecordId, b.Title, m.FullName, br.Duedate,
                    julianday('now') - julianday(br.Duedate) AS DelayDays
                    FROM BorrowRecord br
                    JOIN Book b ON br.BookId = b.BookId
                    JOIN Members m ON br.MemberId = m.MemberId
                    WHERE br.Returndate IS NULL AND br.Duedate < date('now')";

            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int delay = Convert.ToInt32(reader["DelayDays"]);
                    int fine = delay * 1;

                    Console.WriteLine($"Record: {reader["RecordId"]}, Book: {reader["Title"]}, Member: {reader["FullName"]}, Fine: {fine}");
                }
            }
        }


        public static void MostBorrowedBooks(SQLiteConnection connection)
        {
            string query = @"SELECT b.Title, COUNT(*) AS BorrowCount
                     FROM BorrowRecord br
                     JOIN Book b ON br.BookId = b.BookId
                     GROUP BY br.BookId
                     ORDER BY BorrowCount DESC
                     LIMIT 10";

            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Title"]} - {reader["BorrowCount"]} times");
                }
            }
        }

        public static void MostActiveMember(SQLiteConnection connection)
        {
            string query = @"SELECT m.FullName, COUNT(*) AS MemberCount
                     FROM BorrowRecord br
                     JOIN Members m ON br.MemberId = m.MemberId
                     GROUP BY br.MemberId
                     ORDER BY MemberCount DESC
                     LIMIT 10";

            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["FullName"]} - {reader["MemberCount"]} times"); 
                }
            }
        }

        public static void BooksNeverBorrowed(SQLiteConnection connection)
        {
            string query = @"SELECT Title FROM Book WHERE BookId NOT IN (SELECT DISTINCT BOOKID FROM BorrowRecord)";

            using(var cmd = new SQLiteCommand(query, connection))
            using(var reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    Console.WriteLine(reader["Title"]);
                }
            }
            
        }


        public static void CategoryWise(SQLiteConnection connection)
        {
            string query = @"SELECT Category, COUNT(*) AS T
                     FROM Book
                     GROUP BY Category";

            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Category"]} - {reader["T"]}");
                }
            }
        }
        

        public static void IdentifyOverdueBooks(SQLiteConnection connection)
        {
            string query = @"SELECT br.RecordId, b.Title, m.FullName, br.Duedate
                 FROM BorrowRecord br
                 JOIN Book b ON br.BookId = b.BookId
                 JOIN Members m ON br.MemberId = m.MemberId
                 WHERE br.Returndate IS NULL 
                 AND br.Duedate < date('now')";

            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Record: {reader["RecordId"]}, Book: {reader["Title"]}, Member: {reader["FullName"]}, Due: {reader["Duedate"]}");
                }
            }
        }

        public static void IssueBook(SQLiteConnection connection)
        {
            Console.WriteLine("Enter Book ID:");
            int bookId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Member ID:");
            int memberId = int.Parse(Console.ReadLine());

            
            string checkQuery = "SELECT Available FROM Book WHERE BookId = @bookId";

            int available;

            using (var cmd = new SQLiteCommand(checkQuery, connection))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine("Book not found");
                    return;
                }

                available = Convert.ToInt32(result);

                if (available <= 0)
                {
                    Console.WriteLine("No copies available");
                    return;
                }
            }

            DateTime borrowDate = DateTime.Now;
            DateTime dueDate = borrowDate.AddDays(14);

            
            string insertQuery = @"INSERT INTO BorrowRecord 
        (BookId, MemberId, BorrowDate, Duedate, Returndate, Fine)
        VALUES (@bookId, @memberId, @borrowDate, @dueDate, NULL, 0)";

            using (var cmd = new SQLiteCommand(insertQuery, connection))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@memberId", memberId);
                cmd.Parameters.AddWithValue("@borrowDate", borrowDate);
                cmd.Parameters.AddWithValue("@dueDate", dueDate);

                cmd.ExecuteNonQuery();
            }

            
            string updateBook = "UPDATE Book SET Available = Available - 1 WHERE BookId = @bookId";

            using (var cmd = new SQLiteCommand(updateBook, connection))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine($"Book issued! Due date: {dueDate.ToShortDateString()}");
        }



        public static void ReturnBook(SQLiteConnection connection)
        {
            Console.WriteLine("Enter Record ID:");
            int recordId = int.Parse(Console.ReadLine());

            int bookId;

            
            string query = "SELECT BookId FROM BorrowRecord WHERE RecordId = @id AND Returndate IS NULL";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", recordId);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine("Invalid record");
                    return;
                }

                bookId = Convert.ToInt32(result);
            }

            DateTime returnDate = DateTime.Now;

            
            string updateRecord = "UPDATE BorrowRecord SET Returndate = @date WHERE RecordId = @id";

            using (var cmd = new SQLiteCommand(updateRecord, connection))
            {
                cmd.Parameters.AddWithValue("@date", returnDate);
                cmd.Parameters.AddWithValue("@id", recordId);
                cmd.ExecuteNonQuery();
            }
          
            string updateBook = "UPDATE Book SET Available = Available + 1 WHERE BookId = @bookId";

            using (var cmd = new SQLiteCommand(updateBook, connection))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Book returned successfully!");
        }
    }  
}
