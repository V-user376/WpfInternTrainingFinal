using System;
using System.Data.SQLite;

namespace POC07_LibraryManagement.Models
{
    public class Member
    {
        
        public int MemberId { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime MembershipDate { get; set; }
        public bool IsActive { get; set; }

        
        public static void CreateTable(SQLiteConnection connection)
        {
            string query = @"CREATE TABLE IF NOT EXISTS Members(
                MemberId INTEGER PRIMARY KEY AUTOINCREMENT,
                MemberCode TEXT UNIQUE NOT NULL, 
                FullName TEXT NOT NULL, 
                Email TEXT,
                Phone TEXT, 
                Address TEXT,
                MembershipDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                IsActive BOOLEAN DEFAULT 1
            )";

            using (var cmd = new SQLiteCommand(query, connection))
                cmd.ExecuteNonQuery();
        }

        
        public void Add(SQLiteConnection connection)
        {
            Console.WriteLine("Enter Member Code:");
            MemberCode = Console.ReadLine();

            Console.WriteLine("Enter Full Name:");
            FullName = Console.ReadLine();

            Console.WriteLine("Enter Email:");
            Email = Console.ReadLine();

            Console.WriteLine("Enter Phone:");
            Phone = Console.ReadLine();

            Console.WriteLine("Enter Address:");
            Address = Console.ReadLine();

            Console.WriteLine("Enter Membership Date (yyyy-MM-dd) or leave blank for today:");
            string dateInput = Console.ReadLine();
            if (!DateTime.TryParse(dateInput, out DateTime dt))
                dt = DateTime.Now;
            MembershipDate = dt;

            IsActive = true;

            string query = @"INSERT INTO Members
                (MemberCode, FullName, Email, Phone, Address, MembershipDate, IsActive)
                VALUES (@code, @name, @email, @phone, @address, @date, @active)";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@code", MemberCode);
                cmd.Parameters.AddWithValue("@name", FullName);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@phone", Phone);
                cmd.Parameters.AddWithValue("@address", Address);
                cmd.Parameters.AddWithValue("@date", MembershipDate);
                cmd.Parameters.AddWithValue("@active", IsActive ? 1 : 0);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Member added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding member: " + ex.Message);
                }
            }
        }

        public static void ShowMembers(SQLiteConnection connection)
        {
            string sqlQuery = "SELECT * FROM Members";

            using (var command = new SQLiteCommand(sqlQuery, connection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                
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
        public static void UpdateMembers(SQLiteConnection connection)
        {
            
            Console.WriteLine("Enter Book ID to update:");
            int selectBookId;
            while (!int.TryParse(Console.ReadLine(), out selectBookId))
            {
                Console.WriteLine("Invalid ID. Enter a valid number:");
            }

            
            Console.WriteLine("Enter new Total Copies:");
            int newTotalValue;
            while (!int.TryParse(Console.ReadLine(), out newTotalValue))
            {
                Console.WriteLine("Invalid number. Enter again:");
            }

            
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

        public static void DeleteMember(SQLiteConnection connection)
        {
            Console.WriteLine("Enter member Id");
            int memberId = int.Parse(Console.ReadLine());

            string checkQuery = "SELECT COUNT(*) FROM Members WHERE MemberId = @memberId";

            using (var cmd = new SQLiteCommand(checkQuery, connection))
            {
                cmd.Parameters.AddWithValue("@memberId", memberId);

                long count = (long)cmd.ExecuteScalar();

                if (count < 0)
                {
                    Console.WriteLine("Cannot delete book");
                    return;
                }
            }

            string deleteQuery = "DELETE FROM Members WHERE MemberId = @memberId";

            using (var cmd = new SQLiteCommand(deleteQuery, connection))
            {
                cmd.Parameters.AddWithValue("@memberId", memberId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Member deleted");

            }

        }




        public static void TotalMembers(SQLiteConnection connection)
        {
            string query = "SELECT COUNT (*) FROM Members";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"total members {count}");
            }
        }



    }
}