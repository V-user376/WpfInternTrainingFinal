using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL; 

public class Student
{
    public int RollNumber { get; set; }
    public string Name { get; set; }
    public int MathMarks { get; set; }
    public int ScienceMarks { get; set; }
    public int EnglishMarks { get; set; }
    public int TotalMarks { get; set; }
    public double Percentage { get; set; }
    public string Grade { get; set; }

}


namespace StudentResult
{
    internal class Program
    {
        static List<Student> students = new List<Student>();
        
        static void Main(string[] args)
        {
            //AddStudent();

            while (true)
            {
                
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. View All Students");
                               
                Console.WriteLine("Select operation (1-2): \n");
                string userChoice = Console.ReadLine();
                switch (userChoice)

                {
                    case "1":
                        //Program p = new Program();
                        //p.AddEmployee();                          can be called by making the instance of class
                        AddStudent();
                        break;
                    case "2":
                        ViewStudent();
                        break;
                    default:
                        Console.WriteLine("Enter number 1-6");
                        break;
                }
            }

            int i = 2;
            WorkBook workBook = WorkBook.Create();
            WorkSheet sheet = workBook.CreateWorkSheet("Sheet1");

            sheet["A1"].Value = "RollNumber";
            sheet["A2"].Value = "Name";
            sheet["A3"].Value = "MathMarks";
            sheet["A4"].Value = "ScienceMarks";
            sheet["A5"].Value = "EnglishMarks";
            sheet["A6"].Value = "Percentage";
            sheet["A7"].Value = "Grade";

            foreach(var  student in students)
            {
                sheet["A" + i].Value = student.RollNumber;
                sheet["B" + i].Value = student.Name;
                sheet["C" + i].Value = student.MathMarks;
                sheet["D" + i].Value = student.ScienceMarks;
                sheet["E" + i].Value = student.EnglishMarks;
                sheet["F" + i].Value = student.Percentage;
                sheet["G" + i].Value = student.Grade;
                i++;
            }

            workBook.SaveAsCsv("data.csv", ",");

        }

        static void AddStudent()
        {
            Console.WriteLine("Enter your RollNumber");
            var rollnum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your Name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter your Mathmarks");
            int mathMarks = Convert.ToInt32(Console.ReadLine());
            mathMarks = MarksValidator(mathMarks);

            Console.WriteLine("Enter your Sciencemarks");
            var scienceMarks = Convert.ToInt32(Console.ReadLine());
            scienceMarks = MarksValidator(scienceMarks);
            
            Console.WriteLine("Enter your Englishmarks");
            var englishMarks = Convert.ToInt32(Console.ReadLine());
            englishMarks = MarksValidator(englishMarks); 

            
            var totalMarks = mathMarks + scienceMarks + englishMarks;
            //Console.WriteLine($"Totalmarks = {totalMarks}");

            
            double percentagge = (double)totalMarks/300*100;
            //Console.WriteLine($"Percentage = {percentage}");
            double percentage = Math.Round(percentagge, 2);


            //Console.WriteLine("Enter your Grade");
            var grade = "";
            if (percentage >= 90 && percentage <= 100)
            {
                grade = "A";
            }
            else if (percentage >= 75 && percentage <= 89)
            {
                grade = "B";
            }
            else if (percentage >= 60 && percentage <= 74)
            {
                grade = "C";
            }
            else if (percentage >= 40 && percentage <= 59)
            {
                grade = "D";
            }
            else 
            {
                grade = "F";
            }

            students.Add(new Student() { RollNumber = rollnum, Name = name, MathMarks = mathMarks, ScienceMarks = scienceMarks, EnglishMarks = englishMarks, TotalMarks = totalMarks, Percentage = percentage, Grade = grade });

            //Console.WriteLine($"{rollnum}|{name}|{mathMarks}|{scienceMarks}|{englishMarks}|{totalMarks}|{grade}|{percentage}");
        }

        static void ViewStudent()
        {

            List<Student> students = new List<Student>
            {
                new Student { RollNumber = 1, Name = "user1", MathMarks = 40, ScienceMarks = 56, EnglishMarks = 50, TotalMarks = 146, Percentage = 48.66, Grade = "D" },
                new Student { RollNumber = 2, Name = "user2", MathMarks = 80, ScienceMarks = 6, EnglishMarks = 33, TotalMarks = 119, Percentage = 39.66, Grade = "F" },
                new Student { RollNumber = 3, Name = "user3", MathMarks = 90, ScienceMarks = 91, EnglishMarks = 92, TotalMarks = 273, Percentage = 91, Grade = "A" }
            };


            Console.WriteLine("=== Student Result Sheet ===");
            Console.WriteLine("{0,-5} | {1,-20} | {2,-5}| {3,-5}| {4,-5}| {5,-5}| {6,-10}| {7,-5}", "Roll", "Name", "Maths", "Sci", "Eng", "Total", "%", "Grade");
            Console.WriteLine(new string('-', 80));
            foreach (var s in students)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-5}| {3,-5}| {4,-5}| {5,-5}| {6,-10}| {7,-5}", s.RollNumber, s.Name, s.MathMarks, s.ScienceMarks, s.EnglishMarks, s.TotalMarks, s.Percentage, s.Grade);
            }
            Console.WriteLine(new string('-', 80));
            Console.WriteLine("=== Class Statistics ===");
            var totalStudents = students.Count();
            Console.WriteLine($"Total Students: {totalStudents}");


            var averagePercentages = Math.Round(students.Select(s => s.Percentage).Average(),2);                       
            Console.WriteLine($"Average Percentage = {averagePercentages}");

            var highestMarks = students.Select(s => s.TotalMarks).Max();
            Console.WriteLine($"Highest Total {highestMarks}");

            Type type = highestMarks.GetType();
            Console.WriteLine(type);


            //var nameWithHighestMarks = from s in students
            //                           where s.TotalMarks == highestMarks select highestMarks.ame;

            //Console.WriteLine(nameWithHighestMarks);
            //var namess = students.Where(s => s.Name.)

            

            Console.WriteLine("\nSubject Toppers");

           
            
            var mat = students.Max(p=>p.MathMarks);
            //Console.WriteLine(mat);
            //var highestMathMarks = students.Where(p => p.MathMarks == mat).Select(p => p.Name).ToList();

            string highestMathMarks = students.FirstOrDefault(p => p.MathMarks == mat)?.Name;
            //Console.WriteLine($"{highestMathMarks}");
            //Console.WriteLine(hhighestMathMarks);
            Console.WriteLine($"-Maths {highestMathMarks} {mat}");

            var maxScience = students.Max(p => p.ScienceMarks);
            string maxScienceName = students.FirstOrDefault(p => p.ScienceMarks == maxScience)?.Name;
            Console.WriteLine($"-Science {maxScienceName} {maxScience}");

            var maxEnglish = students.Max(p => p.EnglishMarks);
            string maxEnglishName = students.FirstOrDefault(p => p.EnglishMarks == maxEnglish)?.Name;
            Console.WriteLine($"-English {maxEnglishName} {maxEnglish}"); 

            //var maxEnglishName = students.Where(p => p.EnglishMarks == maxEnglish).Select(p => p.Name).ToList();    // this method is used when there are multiple names 
            //Console.WriteLine(string.Join(" , ", maxEnglishName));




            Console.WriteLine(new string('-', 40));

            var groupedStudents = students.GroupBy(s => s.Grade).ToList();
            
            foreach( var groupedStudent in groupedStudents)
            {
                Console.WriteLine($"{groupedStudent.Key} {groupedStudent.Count()}");  
            }
            Console.WriteLine(new string('-', 40));

            var failingStudents = students.Where(p => p.MathMarks < 40 || p.EnglishMarks < 40 || p.ScienceMarks < 40).ToList();
            foreach(var student in failingStudents)
            {
                Console.WriteLine(student.Name);
            }




            Console.ReadLine();
        }

        public static int MarksValidator(int a)
        {
            bool isValid = false;
            while (!isValid)
            {
                if (a>0 && a<100)
                {
                    isValid = true;                    
                }
                else
                {
                    Console.WriteLine("Enter marks between 0 to 100");
                    a = Convert.ToInt32(Console.ReadLine());
                }
        }
            return a;
        }
    }
}
