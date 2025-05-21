using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace GradingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewStudentRecord();
                        break;
                    case "2":
                        EnterMarksForStudent();
                        break;
                    case "3":
                        UpdateStudentMarks();
                        break;
                    case "4":
                        ShowStudentRecord();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("===== Student Grading System =====");
            Console.WriteLine("1. Create a new student record");
            Console.WriteLine("2. Enter marks for a student");
            Console.WriteLine("3. Update a student's marks");
            Console.WriteLine("4. Show a student record");
            Console.WriteLine("5. Quit");
            Console.Write("\nEnter your choice (1-5): ");
        }

        static void CreateNewStudentRecord()
        {
            string studentNumber = GenerateStudentNumber();
            
            Console.WriteLine($"Generated Student Number: {studentNumber}");
            Console.Write("Enter student name: ");
            string studentName = Console.ReadLine();

            string recordsDirectory = "StudentRecords";
            if (!Directory.Exists(recordsDirectory))
            {
                Directory.CreateDirectory(recordsDirectory);
            }

            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");
            
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Student Number: {studentNumber}");
                writer.WriteLine($"Student Name: {studentName}");
                writer.WriteLine("No marks entered yet.");
            }

            Console.WriteLine($"\nStudent record created successfully for {studentName} with Student Number {studentNumber}.");
        }

        static string GenerateStudentNumber()
        {
            string recordsDirectory = "StudentRecords";
            if (!Directory.Exists(recordsDirectory))
            {
                Directory.CreateDirectory(recordsDirectory);
            }

            Random random = new Random();
            string studentNumber;
            
            do
            {
                // Generate 8-digit student number
                studentNumber = random.Next(10000000, 99999999).ToString();
            } while (File.Exists(Path.Combine(recordsDirectory, $"{studentNumber}.txt")));

            return studentNumber;
        }

        static void EnterMarksForStudent()
        {
            Console.Write("Enter student number: ");
            string studentNumber = Console.ReadLine();

            string recordsDirectory = "StudentRecords";
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");

            if (File.Exists(filePath))
            {
                // Read existing student data
                List<string> existingData = File.ReadAllLines(filePath).ToList();
                string studentName = existingData[1].Replace("Student Name: ", "");

                Console.WriteLine($"Entering marks for {studentName} (Student Number: {studentNumber})");
                
                int[] marks = GetMarksFromUser();
                double average = marks.Average();
                string result = average >= 40 ? "PASSED" : "FAILED";

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write back the student information
                    writer.WriteLine(existingData[0]); // Student Number
                    writer.WriteLine(existingData[1]); // Student Name
                    
                    // Write the new marks
                    writer.WriteLine($"--- Marks Entry: {DateTime.Now} ---");
                    for (int i = 0; i < marks.Length; i++)
                    {
                        writer.WriteLine($"Mark {i + 1}: {marks[i]}");
                    }
                    writer.WriteLine($"Average: {average:F2}");
                    writer.WriteLine($"Result: {result}");
                }

                Console.WriteLine($"\nMarks entered successfully for student {studentNumber}.");
                Console.WriteLine($"Average: {average:F2}");
                Console.WriteLine($"Result: {result}");
            }
            else
            {
                Console.WriteLine($"Student record with number {studentNumber} not found.");
            }
        }

        static void UpdateStudentMarks()
        {
            Console.Write("Enter student number: ");
            string studentNumber = Console.ReadLine();

            string recordsDirectory = "StudentRecords";
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");

            if (File.Exists(filePath))
            {
                // Read existing student data
                List<string> existingData = File.ReadAllLines(filePath).ToList();
                string studentName = existingData[1].Replace("Student Name: ", "");

                Console.WriteLine($"Updating marks for {studentName} (Student Number: {studentNumber})");
                
                int[] marks = GetMarksFromUser();
                double average = marks.Average();
                string result = average >= 40 ? "PASSED" : "FAILED";

                using (StreamWriter writer = new StreamWriter(filePath, append: false))
                {
                    // Write back the original student information headers
                    writer.WriteLine(existingData[0]); // Student Number
                    writer.WriteLine(existingData[1]); // Student Name
                    
                    // Write previously existing data
                    for (int i = 2; i < existingData.Count; i++)
                    {
                        writer.WriteLine(existingData[i]);
                    }
                    
                    // Write the new marks
                    writer.WriteLine("\n--- Updated Marks Entry: " + DateTime.Now + " ---");
                    for (int i = 0; i < marks.Length; i++)
                    {
                        writer.WriteLine($"Mark {i + 1}: {marks[i]}");
                    }
                    writer.WriteLine($"Average: {average:F2}");
                    writer.WriteLine($"Result: {result}");
                }

                Console.WriteLine($"\nMarks updated successfully for student {studentNumber}.");
                Console.WriteLine($"Average: {average:F2}");
                Console.WriteLine($"Result: {result}");
            }
            else
            {
                Console.WriteLine($"Student record with number {studentNumber} not found.");
            }
        }

        static void ShowStudentRecord()
        {
            Console.Write("Enter student number: ");
            string studentNumber = Console.ReadLine();

            string recordsDirectory = "StudentRecords";
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");

            if (File.Exists(filePath))
            {
                Console.WriteLine("\n===== Student Record =====");
                string[] recordData = File.ReadAllLines(filePath);
                foreach (string line in recordData)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine($"Student record with number {studentNumber} not found.");
            }
        }

        static int[] GetMarksFromUser()
        {
            int[] marks = new int[6];
            
            for (int i = 0; i < 6; i++)
            {
                bool validMark = false;
                while (!validMark)
                {
                    Console.Write($"Enter mark {i + 1} (0-100): ");
                    if (int.TryParse(Console.ReadLine(), out int mark) && mark >= 0 && mark <= 100)
                    {
                        marks[i] = mark;
                        validMark = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid mark. Please enter a number between 0 and 100.");
                    }
                }
            }
            
            return marks;
        }
    }
} 