using System;
using System.Collections.Generic;
using System.IO;

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
            Console.WriteLine("4. Edit task");
            Console.WriteLine("5. Clear all tasks");
            Console.WriteLine("6. Show task count");
            Console.WriteLine("7. Exit");

            while (true)
            {
                Console.Write("\nChoose an action (1-7): ");

                bool result = int.TryParse(Console.ReadLine(), out WhatNeed);

                if (result && WhatNeed > 0 && WhatNeed <= 7)
                {
                    Console.WriteLine($"You selected: {WhatNeed}");
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
            }

            switch (WhatNeed)
            {
                case 1:
                    Console.WriteLine("Displaying your tasks...");
                    ShowTasks();
                    break;
                case 2:
                    Console.Write("Enter the task name: ");
                    string taskName = Console.ReadLine();
                    AddTask(taskName);
                    break;
                case 3:
                    Console.WriteLine("Enter the number of the task to delete:");
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
                case 4:
                    Console.WriteLine("Enter the number of the task to edit:");
                    ShowTasks();
                    if (int.TryParse(Console.ReadLine(), out int taskToEdit))
                    {
                        EditTask(taskToEdit);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                    break;
                case 5:
                    Console.WriteLine("Clearing all tasks...");
                    ClearAllTasks();
                    break;
                case 6:
                    Console.WriteLine("Calculating task count...");
                    ShowTaskCount();
                    break;
                case 7:
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return;
            }
            Console.ReadLine();
        }

        
        static void ShowTasks()
        {
            try
            {
                if (File.Exists("tasks.txt"))
                {
                    using (StreamReader streamReader = new StreamReader("tasks.txt"))
                    {
                        string line;
                        int count = 1;
                        Console.WriteLine("Your tasks:");
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            Console.WriteLine($"{count}. {line}");
                            count++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No tasks found.");
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while reading tasks.");
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
                Console.WriteLine("Task added successfully.");
            }
            catch
            {
                Console.WriteLine("An error occurred while adding the task.");
            }
        }

        
        static void DeleteTask(int taskNumber)
        {
            string filePath = "tasks.txt";
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No tasks found.");
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

       
        static void EditTask(int taskNumber)
        {
            string filePath = "tasks.txt";
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No tasks found.");
                    return;
                }

                List<string> tasks = new List<string>(File.ReadAllLines(filePath));

                if (taskNumber < 1 || taskNumber > tasks.Count)
                {
                    Console.WriteLine("Invalid task number.");
                    return;
                }

                Console.Write("Enter the new description for the task: ");
                string newDescription = Console.ReadLine();

                tasks[taskNumber - 1] = newDescription; 
                File.WriteAllLines(filePath, tasks); 
                Console.WriteLine("Task updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

      
        static void ClearAllTasks()
        {
            string filePath = "tasks.txt";
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath); 
                    Console.WriteLine("All tasks cleared.");
                }
                else
                {
                    Console.WriteLine("No tasks to clear.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

       
        static void ShowTaskCount()
        {
            string filePath = "tasks.txt";
            try
            {
                if (File.Exists(filePath))
                {
                    int count = File.ReadAllLines(filePath).Length;
                    Console.WriteLine($"You have {count} task(s).");
                }
                else
                {
                    Console.WriteLine("You have no tasks.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
