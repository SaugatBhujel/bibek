using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace GradingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcomeScreen();
            
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nExiting program. Thank you for using Student Grading System!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                if (!exit)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            
            string[] welcomeArt = {
                @"   _____ _             _            _     _____               _ _             ",
                @"  / ____| |           | |          | |   / ____|             | (_)            ",
                @" | (___ | |_ _   _  __| | ___ _ __ | |_ | |  __ _ __ __ _  __| |_ _ __   __ _ ",
                @"  \___ \| __| | | |/ _` |/ _ \ '_ \| __|| | |_ | '__/ _` |/ _` | | '_ \ / _` |",
                @"  ____) | |_| |_| | (_| |  __/ | | | |_ | |__| | | | (_| | (_| | | | | | (_| |",
                @" |_____/ \__|\__,_|\__,_|\___|_| |_|\__(_)_____|_|  \__,_|\__,_|_|_| |_|\__, |",
                @"                                                                          __/ |",
                @"                                                                         |___/ ",
                @"                                _____           _                              ",
                @"                               / ____|         | |                             ",
                @"                              | (___  _   _ ___| |_ ___ _ __ ___              ",
                @"                               \___ \| | | / __| __/ _ \ '_ ` _ \             ",
                @"                               ____) | |_| \__ \ ||  __/ | | | | |            ",
                @"                              |_____/ \__, |___/\__\___|_| |_| |_|            ",
                @"                                       __/ |                                   ",
                @"                                      |___/                                    "
            };

            foreach (string line in welcomeArt)
            {
                Console.WriteLine(line);
                Thread.Sleep(50); // Small delay for animation effect
            }
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n                  Welcome to the Student Grading System");
            Console.WriteLine("                  ====================================");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n     This application helps you manage student records and their grades.");
            Console.WriteLine("     You can create new records, enter marks, update marks, and view records.");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n                Press any key to continue to the main menu...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        static void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║       STUDENT GRADING SYSTEM          ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  1. Create a new student record");
            Console.WriteLine("  2. Enter marks for a student");
            Console.WriteLine("  3. Update a student's marks");
            Console.WriteLine("  4. Show a student record");
            Console.WriteLine("  5. Quit");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nEnter your choice (1-5): ");
            Console.ResetColor();
        }

        static void CreateNewStudentRecord()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║       CREATE NEW STUDENT RECORD       ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            string studentNumber = GenerateStudentNumber();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nGenerated Student Number: {studentNumber}");
            Console.ResetColor();
            
            Console.Write("\nEnter student name: ");
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nStudent record created successfully!");
            Console.WriteLine($"Name: {studentName}");
            Console.WriteLine($"Student Number: {studentNumber}");
            Console.ResetColor();
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
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║         ENTER STUDENT MARKS          ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nEnter student number: ");
            string studentNumber = Console.ReadLine();

            string recordsDirectory = "StudentRecords";
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");

            if (File.Exists(filePath))
            {
                // Read existing student data
                List<string> existingData = File.ReadAllLines(filePath).ToList();
                string studentName = existingData[1].Replace("Student Name: ", "");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nEntering marks for {studentName} (Student Number: {studentNumber})");
                Console.ResetColor();
                
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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nMarks entered successfully for {studentName}.");
                Console.WriteLine($"Average: {average:F2}");
                Console.ForegroundColor = result == "PASSED" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Result: {result}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nStudent record with number {studentNumber} not found.");
                Console.ResetColor();
            }
        }

        static void UpdateStudentMarks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║         UPDATE STUDENT MARKS         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nEnter student number: ");
            string studentNumber = Console.ReadLine();

            string recordsDirectory = "StudentRecords";
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");

            if (File.Exists(filePath))
            {
                // Read existing student data
                List<string> existingData = File.ReadAllLines(filePath).ToList();
                string studentName = existingData[1].Replace("Student Name: ", "");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nUpdating marks for {studentName} (Student Number: {studentNumber})");
                Console.ResetColor();
                
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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nMarks updated successfully for {studentName}.");
                Console.WriteLine($"Average: {average:F2}");
                Console.ForegroundColor = result == "PASSED" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Result: {result}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nStudent record with number {studentNumber} not found.");
                Console.ResetColor();
            }
        }

        static void ShowStudentRecord()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║          VIEW STUDENT RECORD         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nEnter student number: ");
            string studentNumber = Console.ReadLine();

            string recordsDirectory = "StudentRecords";
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");

            if (File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n╔═══════════════════════════════════════╗");
                Console.WriteLine("║          STUDENT RECORD DATA          ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.ResetColor();
                
                string[] recordData = File.ReadAllLines(filePath);
                
                // Display student info with color formatting
                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = 0; i < 2; i++) // Student number and name
                {
                    Console.WriteLine(recordData[i]);
                }
                Console.ResetColor();
                
                // Display marks and results with appropriate colors
                for (int i = 2; i < recordData.Length; i++)
                {
                    if (recordData[i].Contains("--- Marks Entry") || recordData[i].Contains("--- Updated Marks Entry"))
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(recordData[i]);
                        Console.ResetColor();
                    }
                    else if (recordData[i].Contains("Average:"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(recordData[i]);
                        Console.ResetColor();
                    }
                    else if (recordData[i].Contains("Result:"))
                    {
                        if (recordData[i].Contains("PASSED"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine(recordData[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(recordData[i]);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nStudent record with number {studentNumber} not found.");
                Console.ResetColor();
            }
        }

        static int[] GetMarksFromUser()
        {
            int[] marks = new int[6];
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPlease enter 6 marks (0-100):");
            Console.ResetColor();
            
            for (int i = 0; i < 6; i++)
            {
                bool validMark = false;
                while (!validMark)
                {
                    Console.Write($"  Mark {i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int mark) && mark >= 0 && mark <= 100)
                    {
                        marks[i] = mark;
                        validMark = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  Invalid mark. Please enter a number between 0 and 100.");
                        Console.ResetColor();
                    }
                }
            }
            
            return marks;
        }
    }
} 