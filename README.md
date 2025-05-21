# Student Grading Application

A console-based application for managing student grades.

## Features

- Create new student records with unique student IDs
- Enter marks for students
- Update student marks
- View student records
- Track student progress with pass/fail results

## How to Run

1. Make sure you have .NET SDK installed
2. Open a terminal/command prompt in the project directory
3. Run the application with:
   ```
   dotnet run
   ```

## Usage Instructions

### Creating a New Student Record
1. Select option 1 from the menu
2. The system will generate a unique 8-digit student number
3. Enter the student's name when prompted
4. The system will create a blank student record

### Entering Marks for a Student
1. Select option 2 from the menu
2. Enter the student number when prompted
3. If the record exists, enter 6 marks (0-100) when prompted
4. The system will calculate the average and determine if the student passed (≥40) or failed (<40)

### Updating a Student's Marks
1. Select option 3 from the menu
2. Enter the student number when prompted
3. If the record exists, enter 6 marks (0-100) when prompted
4. The new marks will be appended to the existing record, preserving the history

### Viewing a Student Record
1. Select option 4 from the menu
2. Enter the student number when prompted
3. If the record exists, the system will display all details from the record

### Exiting the Program
1. Select option 5 from the menu 