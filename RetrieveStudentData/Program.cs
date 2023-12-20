using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrieveStudentData
{

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Age: {Age}";
        }
    }



    class Program
    {

        static List<Student> ReadStudentDataFromFile(string filePath)
        {
            List<Student> students = new List<Student>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {

                    // Skip lines that are not in the expected format
                    if (!line.StartsWith("ID:"))
                    {
                        Console.WriteLine($" {line}");
                        continue;
                    }


                    string[] data = line.Split(':');

                    if (data.Length == 3) // Ensure there are three parts: ID, Name, Age
                    {
                        if (int.TryParse(data[1], out int id) &&
                            int.TryParse(data[3], out int age))
                        {
                            string name = data[2].Trim(); // trim leading and trailing spaces

                            students.Add(new Student { Id = id, Name = name, Age = age });
                        }
                        else
                        {
                            Console.WriteLine($"Error parsing ID or age in line: {line}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid data format in line: {line}");
                    }
                }

                if (students.Count == 0)
                {

                    Console.WriteLine("No valid student data found in the file.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data from file: {ex.Message}");
            }

            return students;
        }


        static void DisplayStudentData(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }



       

        static void Main()
        {

            {
                // Student data content
                string studentDataContent = "Student Data:\n\n" +
                    "ID= 1, Name= Rahman, Age= 25\n" +
                    "ID= 2, Name= Reshma, Age= 22\n" +
                    "ID= 3, Name= Rashmi, Age= 20";

                // Write student data to Notepad
                WriteToNotepad(studentDataContent);


                

            }


            string filePath = "F:\\MPHASIS DOCUMENTS\\DEC19 PROJECT\\RetrieveStudentData\\RetrieveStudentData\\bin\\Debug\\students.txt"; // Update with the actual file path

            List<Student> students = ReadStudentDataFromFile(filePath);


            if (students.Count > 0)
            {
                Console.WriteLine("students data:");
                DisplayStudentData(students);

                // Write data to a temporary text file
                string tempFilePath = Path.Combine(Path.GetTempPath(), "F:\\MPHASIS DOCUMENTS\\DEC19 PROJECT\\RetrieveStudentData\\RetrieveStudentData\\bin\\Debug\\students.txt");
                WriteDataToTextFile(students, tempFilePath);

                // Open the temporary text file with Notepad
                OpenNotepad(tempFilePath);
            }
            else
            {
                Console.WriteLine("No student data found.");
            }
        }


        static void WriteToNotepad(string content)
        {
            try
            {
                // Start Notepad process
                Process notepadProcess = new Process();
                notepadProcess.StartInfo.FileName = "notepad.exe";
                notepadProcess.StartInfo.UseShellExecute = false;
                notepadProcess.StartInfo.RedirectStandardInput = true;
                notepadProcess.Start();

                // Write content to Notepad
                StreamWriter sw = notepadProcess.StandardInput;
                sw.WriteLine(content);

                // Close StreamWriter and wait for Notepad to exit
                //sw.Close();
                notepadProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to Notepad: {ex.Message}");
            }
        }



        static void WriteDataToTextFile(List<Student> students, string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var student in students)
                    {
                        sw.WriteLine(student);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing data to file: {ex.Message}");
            }
        }

        static void OpenNotepad(string filePath)
        {
            try
            {
                Process.Start("notepad.exe", filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening Notepad: {ex.Message}");
            }
        }

    }
}
