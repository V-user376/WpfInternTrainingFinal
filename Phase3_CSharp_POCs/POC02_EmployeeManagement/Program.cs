using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.RegularExpressions;
//using System.Text.Json;



public class Employee
{
    //private decimal salary;
    public int ID {get; set;}
    
    public string Name { get; set; }
    public string Department {get; set;}


    public decimal Salary { get; set; }
//    public decimal Salary //{ get; set; }
//    {
//        get { return salary; }
//        set
//        {
//            if (value < 0)
//            {
//                throw new ArgumentException("Salary cannot be negative", nameof(Salary));
//            }
//            salary = value;
//        }
//    }
    public DateTime JoiningData {get; set;}
}


namespace EmployeeManagementSystem
{
    internal class Program
    {
        
        






    // from here start

    //static List<Employee> employees = new List<Employee>();        
        static int studentID = 1;
        static List<Employee> employees = new List<Employee>
        {
            new Employee { ID = 1, Name = "", Department = "HR", Salary = 10.5m, JoiningData = DateTime.Now },
            new Employee { ID = 2, Name = "Mouse", Department = "Developer", Salary = 20.0m, JoiningData = DateTime.Now.AddHours(2) },
            new Employee { ID = 3, Name = "Keyboard", Department = "Employee", Salary = 31.0m, JoiningData = DateTime.Parse("2025-10-25 10:00 AM") },
        //    //new Employee { ID = 4, Name = "speaker", Department = "Inter", Salary = 30.50m }
        };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\x1b[1m\n=== Employee Management System === \x1b[0m");
                Console.WriteLine("1. Add New Employee");
                Console.WriteLine("2. View All Employees");
                Console.WriteLine("3. Search Employee by ID");
                Console.WriteLine("4. Update Employee  Details");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Exit");
                Console.WriteLine("7. Sort Employees by salary");
                Console.WriteLine("8. Load data to txt file");

                Console.WriteLine("Select operation (1-8): \n");
                string userChoice = Console.ReadLine();
                switch (userChoice)

                {
                    case "1":
                        //Program p = new Program();
                        //p.AddEmployee();                          can be called by making the instance of class
                        AddEmployee();
                        break;
                    case "2":
                        ViewEmployee();
                        break;
                    case "3":
                        SearchEmployee();
                        break;
                    case "4":
                        UpdateEmployee();
                        break;
                    case "5":
                        DeleteEmployee();
                        break;
                    case "6":
                        System.Environment.Exit(0);
                        break;
                    case "7":
                        SortSalary();
                        break;
                    case "8":
                        LoadDataToTextFile();
                        break;
                    default:
                        Console.WriteLine("Enter number 1-6");
                        break;
                }
            }
        }
        static void AddEmployee()
        {

            ////Console.Write("Enter your name: ");
            //var name = "";

            //while (string.IsNullOrEmpty(name))
            //{
            //    Console.WriteLine("Enter your name");
            //    name = Console.ReadLine();
            //    if(string.IsNullOrEmpty(name))
            //    {
            //        Console.WriteLine("Name cannot be empty. Try again");
            //    }
            //}


            //Console.Write("Enter your dapartment: ");
            //string department = Console.ReadLine();
            //Console.Write("Enter your Salary: ");
            //decimal salary = Convert.ToDecimal(Console.ReadLine());

            //Console.Write("Enter your joiningdate: ");
            //var date = Convert.ToDateTime(Console.ReadLine());

            ////Console.WriteLine($"{name},{salary},{department},{date}");
            //employees.Add(new Employee() { ID = studentID++, Name = name, Department = department, Salary = salary, JoiningData = date });

        }





        static void ViewEmployee()
        {
            Console.WriteLine("=== All Employees ===");
            Console.WriteLine("{0,-5} | {1,-20} | {2,-20}| {3,-20}| {4,-20}", "ID", "Name", "Department", "Salary", "JoiningDate");
            Console.WriteLine(new string('-', 95));
            foreach (var s in employees)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20}| {3,-20}| {4,-20}",
                                    s.ID, s.Name, s.Department, s.Salary, s.JoiningData);
            }
            Console.WriteLine(new string('-', 95));
            int totalEmployees = studentID - 1;
            Console.WriteLine($"Total Employees: {totalEmployees}");

            decimal totalSalary = 0.0m;
            foreach (Employee s in employees)
            {
                totalSalary += s.Salary;

                //Console.WriteLine($"{total} fwef");
            }
            Console.WriteLine(totalSalary);                                  //this is the foreach method to calculate total salary 

            //decimal totalSalary = employees.Sum(e => e.Salary);           this is the LINQ method to calculate total salary 
            //Console.WriteLine(totalSalary);

        }
        static void SearchEmployee()
        {
            int searchID = Convert.ToInt32(Console.ReadLine());

            var inputID = employees.Where(a => a.ID == searchID).ToList();
            foreach (var s in inputID)
            {
                Console.WriteLine($"Employee of searched ID is {s.Name}");
            }

            //if condition if user entered id of user doesn't exist
        }

        static void UpdateEmployee()
        {

            Console.WriteLine("\nEnter the ID of student to edit details");
            string numbersToFind = Console.ReadLine();
            int numberToFind = int.Parse(numbersToFind);

            Employee foundStudent = employees.FirstOrDefault(s => s.ID == numberToFind);
            if (foundStudent != null)
            {
                Console.WriteLine($"Details for {numberToFind}:");
                Console.WriteLine($"Name {foundStudent.Name} ");
                Console.WriteLine($"Department: {foundStudent.Department}");
                Console.WriteLine($"Salary: {foundStudent.Salary}");
                Console.WriteLine($"JoiningDate: {foundStudent.JoiningData}");
                //Console.WriteLine($"Result: {foundStudent.Result}");
            }
            else
            {
                Console.WriteLine($"Student with number '{numberToFind}' not found.");
            }
            Console.WriteLine("Enter the field you want to edit \n ");

            string fieldToEdit = Console.ReadLine();
            dynamic newValue;

            switch (fieldToEdit.ToLower())
            {
                case "name":
                    newValue = Console.ReadLine();
                    foundStudent.Name = newValue;
                    Console.WriteLine($"Your new age is {foundStudent.Name} \n ");
                    //System.Environment.Exit(0);
                    break;

                case "department":

                    newValue = Console.ReadLine();
                    //double d = double.Parse(newValue);
                    foundStudent.Department = newValue;
                    //updateResult(foundStudent.Marks);

                    Console.WriteLine($"Your new marks are {foundStudent.Department} \n ");
                    //Console.WriteLine($"Here is your updated whole list of student {updateResult(foundStudent.Marks)} ");
                    //foundStudent.Result = updateResult(foundStudent.Department);
                    break;

                case "salary":
                    newValue = Convert.ToDecimal(Console.ReadLine());
                    foundStudent.Salary = newValue;
                    Console.WriteLine($"Your new name is {foundStudent.Salary} \n ");
                    break;

                case "joiningdate":
                    newValue = Convert.ToDateTime(Console.ReadLine());
                    foundStudent.JoiningData = newValue;
                    Console.WriteLine($"Your new course is {foundStudent.JoiningData} \n ");
                    break;


                default:
                    Console.WriteLine("You wrote invalid field");
                    break;

            }
            Console.WriteLine($"Here is your edited list {foundStudent.Name}, {foundStudent.Department}, {foundStudent.Salary}, {foundStudent.JoiningData}\n");

        }

        static void DeleteEmployee()
        {
            Console.WriteLine("\nEnter the name of student to delete details");
            string nameToFind = Console.ReadLine();

            Employee deleteStudent = employees.FirstOrDefault(s => s.Name == nameToFind);
            employees.RemoveAt(employees.IndexOf(deleteStudent));


            ViewEmployee();
        }

        static void SortSalary()
        {
            var result = employees.OrderBy(s => s.Salary);
            //foreach(Employee e in result)
            //{
            //    Console.WriteLine(e.ID + "" + e.Name + "" + e.Department + "" + e.Salary + "" + e.JoiningData);
            //}
            Console.WriteLine("{0,-5} | {1,-20} | {2,-20}| {3,-20}| {4,-20}", "ID", "Name", "Department", "Salary", "JoiningDate");
            Console.WriteLine(new string('-', 95));
            foreach (Employee s in result)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20}| {3,-20}| {4,-20}",
                                    s.ID, s.Name, s.Department, s.Salary, s.JoiningData);
            }
            Console.WriteLine(new string('-', 95));
        }

        static void LoadDataToTextFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string path = "employee.json";
            string fullPath = Path.Combine(desktopPath, path);
            //List<Employee> emptyList = new List<Employee>();
            if (!File.Exists(fullPath))
            {
                try
                {
                    using (FileStream fs = File.Create(fullPath))
                    {
                        Console.WriteLine("Create");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            else
            {
                List<string> newList = File.ReadLines(fullPath).ToList();
                foreach (string line in newList)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }


}
