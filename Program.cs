using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace GradingApplication
{
    /// <summary>
    /// Main Program class that handles all functionality of the Student Grading System
    /// This application allows users to create, view, and update student records with grades
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point of the application
        /// Controls the main menu loop and user interaction flow
        /// </summary>
        static void Main(string[] args)
        {
            // Display welcome screen when application starts
            DisplayWelcomeScreen();
            
            // Main application loop - continues until user chooses to exit
            bool exit = false;
            while (!exit)
            {
                // Show the main menu options
                DisplayMenu();
                string choice = Console.ReadLine();

                // Process user's menu selection
                switch (choice)
                {
                    case "1":
                        CreateNewStudentRecord(); // Create a new student record
                        break;
                    case "2":
                        EnterMarksForStudent(); // Enter marks for existing student
                        break;
                    case "3":
                        UpdateStudentMarks(); // Update marks for existing student
                        break;
                    case "4":
                        ShowStudentRecord(); // View a student's record
                        break;
                    case "5":
                        exit = true; // Exit the application
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nExiting program. Thank you for using Student Grading System!");
                        Console.ResetColor();
                        break;
                    default:
                        // Handle invalid menu selections
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                // Pause before returning to the menu (unless exiting)
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

        /// <summary>
        /// Displays the welcome screen with ASCII art and application information
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            
            // ASCII art for the welcome screen
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

            // Display ASCII art with animation effect
            foreach (string line in welcomeArt)
            {
                Console.WriteLine(line);
                Thread.Sleep(50); // Small delay for animation effect
            }
            
            // Display welcome message and application description
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

        /// <summary>
        /// Displays the main menu options for the user
        /// </summary>
        static void DisplayMenu()
        {
            // Display header with decorative border
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║       STUDENT GRADING SYSTEM          ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            // Display menu options
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  1. Create a new student record");
            Console.WriteLine("  2. Enter marks for a student");
            Console.WriteLine("  3. Update a student's marks");
            Console.WriteLine("  4. Show a student record");
            Console.WriteLine("  5. Quit");
            Console.ResetColor();
            
            // Prompt for user input
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nEnter your choice (1-5): ");
            Console.ResetColor();
        }

        /// <summary>
        /// Creates a new student record with a unique student number
        /// </summary>
        static void CreateNewStudentRecord()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║       CREATE NEW STUDENT RECORD       ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            // Generate a unique student number
            string studentNumber = GenerateStudentNumber();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nGenerated Student Number: {studentNumber}");
            Console.ResetColor();
            
            // Get student name from user
            Console.Write("\nEnter student name: ");
            string studentName = Console.ReadLine();

            // Create directory for student records if it doesn't exist
            string recordsDirectory = "StudentRecords";
            if (!Directory.Exists(recordsDirectory))
            {
                Directory.CreateDirectory(recordsDirectory);
            }

            // Create the student record file
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");
            
            // Write initial student information to the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Student Number: {studentNumber}");
                writer.WriteLine($"Student Name: {studentName}");
                writer.WriteLine("No marks entered yet.");
            }

            // Display confirmation message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nStudent record created successfully!");
            Console.WriteLine($"Name: {studentName}");
            Console.WriteLine($"Student Number: {studentNumber}");
            Console.ResetColor();
        }

        /// <summary>
        /// Generates a unique 8-digit student number
        /// Ensures the number doesn't already exist in the system
        /// </summary>
        /// <returns>A unique student number</returns>
        static string GenerateStudentNumber()
        {
            // Create directory for student records if it doesn't exist
            string recordsDirectory = "StudentRecords";
            if (!Directory.Exists(recordsDirectory))
            {
                Directory.CreateDirectory(recordsDirectory);
            }

            Random random = new Random();
            string studentNumber;
            
            // Generate numbers until we find one that doesn't exist
            do
            {
                // Generate 8-digit student number (between 10000000 and 99999999)
                studentNumber = random.Next(10000000, 99999999).ToString();
            } while (File.Exists(Path.Combine(recordsDirectory, $"{studentNumber}.txt")));

            return studentNumber;
        }

        /// <summary>
        /// Enters marks for an existing student record
        /// </summary>
        static void EnterMarksForStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║         ENTER STUDENT MARKS          ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            // Get student number from user
            Console.Write("\nEnter student number: ");
            string studentNumber = Console.ReadLine();

            // Check if student record exists
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
                
                // Get marks from user
                int[] marks = GetMarksFromUser();
                
                // Calculate average and determine pass/fail
                double average = marks.Average();
                string result = average >= 40 ? "PASSED" : "FAILED";

                // Write updated information to file
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

                // Display confirmation and results
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nMarks entered successfully for {studentName}.");
                Console.WriteLine($"Average: {average:F2}");
                Console.ForegroundColor = result == "PASSED" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Result: {result}");
                Console.ResetColor();
            }
            else
            {
                // Display error if student record not found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nStudent record with number {studentNumber} not found.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Updates marks for an existing student record
        /// Preserves previous marks history
        /// </summary>
        static void UpdateStudentMarks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║         UPDATE STUDENT MARKS         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            // Get student number from user
            Console.Write("\nEnter student number: ");
            string studentNumber = Console.ReadLine();

            // Check if student record exists
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
                
                // Get marks from user
                int[] marks = GetMarksFromUser();
                
                // Calculate average and determine pass/fail
                double average = marks.Average();
                string result = average >= 40 ? "PASSED" : "FAILED";

                // Write updated information to file, preserving history
                using (StreamWriter writer = new StreamWriter(filePath, append: false))
                {
                    // Write back the original student information headers
                    writer.WriteLine(existingData[0]); // Student Number
                    writer.WriteLine(existingData[1]); // Student Name
                    
                    // Write previously existing data (preserving history)
                    for (int i = 2; i < existingData.Count; i++)
                    {
                        writer.WriteLine(existingData[i]);
                    }
                    
                    // Write the new marks as an update
                    writer.WriteLine("\n--- Updated Marks Entry: " + DateTime.Now + " ---");
                    for (int i = 0; i < marks.Length; i++)
                    {
                        writer.WriteLine($"Mark {i + 1}: {marks[i]}");
                    }
                    writer.WriteLine($"Average: {average:F2}");
                    writer.WriteLine($"Result: {result}");
                }

                // Display confirmation and results
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nMarks updated successfully for {studentName}.");
                Console.WriteLine($"Average: {average:F2}");
                Console.ForegroundColor = result == "PASSED" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Result: {result}");
                Console.ResetColor();
            }
            else
            {
                // Display error if student record not found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nStudent record with number {studentNumber} not found.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Shows the complete record for a student
        /// </summary>
        static void ShowStudentRecord()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║          VIEW STUDENT RECORD         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            
            // Get student number from user
            Console.Write("\nEnter student number: ");
            string studentNumber = Console.ReadLine();

            // Check if student record exists
            string recordsDirectory = "StudentRecords";
            string filePath = Path.Combine(recordsDirectory, $"{studentNumber}.txt");

            if (File.Exists(filePath))
            {
                // Display header for student record
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n╔═══════════════════════════════════════╗");
                Console.WriteLine("║          STUDENT RECORD DATA          ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.ResetColor();
                
                // Read and display the student record with formatting
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
                        // Section headers for mark entries
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(recordData[i]);
                        Console.ResetColor();
                    }
                    else if (recordData[i].Contains("Average:"))
                    {
                        // Average marks in green
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(recordData[i]);
                        Console.ResetColor();
                    }
                    else if (recordData[i].Contains("Result:"))
                    {
                        // Pass/fail results with appropriate colors
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
                        // Regular text (marks)
                        Console.WriteLine(recordData[i]);
                    }
                }
            }
            else
            {
                // Display error if student record not found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nStudent record with number {studentNumber} not found.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Gets 6 marks from the user with validation
        /// Ensures marks are between 0 and 100
        /// </summary>
        /// <returns>Array of 6 validated marks</returns>
        static int[] GetMarksFromUser()
        {
            int[] marks = new int[6];
            
            // Prompt for marks entry
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPlease enter 6 marks (0-100):");
            Console.ResetColor();
            
            // Get and validate each mark
            for (int i = 0; i < 6; i++)
            {
                bool validMark = false;
                while (!validMark)
                {
                    Console.Write($"  Mark {i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int mark) && mark >= 0 && mark <= 100)
                    {
                        // Valid mark entered
                        marks[i] = mark;
                        validMark = true;
                    }
                    else
                    {
                        // Invalid mark - show error and prompt again
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