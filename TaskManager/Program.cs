using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Xml.Schema;

namespace TaskManager                                               
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int WhatNeed;
            Console.SetCursorPosition(1, 0);
            Console.WriteLine("Menu");
            Console.WriteLine("1. See my tasks");
            Console.WriteLine("2. Add task");
            Console.WriteLine("3. Delete task");
           

            while (true)
            {
                Console.Write("Choose an action (1-4): ");

                bool result = int.TryParse(Console.ReadLine(), out WhatNeed);

                if (result && WhatNeed > 0 && WhatNeed <= 4)
                {
                    Console.WriteLine($"You selected: {WhatNeed}");
                    break; 


                }    
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
            }
            switch (WhatNeed)
            {
                case 1:
                    Console.WriteLine("Displaying your tasks...");
                    ShowTasks();
                    break;
                case 2:
                    Console.WriteLine("Enter the task name");
                    string TaskName = Console.ReadLine();
                    AddTask(TaskName);
                    string taskDescription = Console.ReadLine();
                    Console.WriteLine("Adding a new task...");
                    
                    break;
                case 3:
                    Console.Write("Enter the number of the task to delete: ");
                    ShowTasks();
                    if (int.TryParse(Console.ReadLine(), out int taskNumber))
                    {
                        DeleteTask(taskNumber);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                    break;
                
            }
        Console.ReadLine();
        }


        static void ShowTasks()
        {
            
            try
            {if (File.Exists("tasks.txt"))
                {
                    using (StreamReader streamReader = new StreamReader("tasks.txt"))
                    {
                        string line;
                        Console.WriteLine("Your tasks:");
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            Console.WriteLine($" - {line}");
                        }
                    }


                }

               
            }
            catch 
            {
                Console.WriteLine($" error occurred");
            }
        }
        static void AddTask(string taskDescription)
        {
            
            try
            {
                
                using (StreamWriter writer = new StreamWriter("tasks.txt", append: true))
                {
                    writer.WriteLine(taskDescription);
                }
                Console.WriteLine("Task added to file successfuly.");
            }
            catch 
            {
                Console.WriteLine($" error ");
            }
        }
        static void DeleteTask(int taskNumber)
        {
            string filePath = "tasks.txt";
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No tasks found. ");
                    return;
                }

                List<string> tasks = new List<string>(File.ReadAllLines(filePath));

                if (taskNumber < 1 || taskNumber > tasks.Count)
                {
                    Console.WriteLine("Invalid task number.");
                    return;
                }

                tasks.RemoveAt(taskNumber - 1); 

                File.WriteAllLines(filePath, tasks); 
                Console.WriteLine("Task deleted successfully.");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
       
        
    }   
}    
 
