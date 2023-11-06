using System;
using System.Collections.Generic;
using Task_4;

namespace TaskSchedulerExample
{
    class Program
    {
        private static TaskScheduler<string, int> taskScheduler;
        private static List<string> executionHistory = new List<string>();

        static void Main(string[] args)
        {
            taskScheduler = new TaskScheduler<string, int>(
                task => task.Length,
                () => Console.ReadLine(),
                task => { });

            taskScheduler.SetPriorityName(1, "Low");
            taskScheduler.SetPriorityName(2, "Medium");
            taskScheduler.SetPriorityName(3, "High");

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Add task");
                Console.WriteLine("2 - Execute next task");
                Console.WriteLine("3 - Execute all tasks");
                Console.WriteLine("4 - Clear queue and reset priorities");
                Console.WriteLine("5 - Clear queue");
                Console.WriteLine("6 - Reset priorities");
                Console.WriteLine("7 - Show execution history");
                Console.WriteLine("8 - Execute tasks");
                Console.WriteLine("9 - Return task to the pool");
                Console.WriteLine("10 - Get task from pool");
                Console.WriteLine("11 - Exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            taskScheduler.AddTaskFromConsole();
                            break;
                        case 2:
                            ExecuteNextTask();
                            break;
                        case 3:
                            ExecuteAllTasks();
                            break;
                        case 4:
                            ClearQueueAndResetPriorities();
                            break;
                        case 5:
                            ClearQueue();
                            break;
                        case 6:
                            ResetPriorities();
                            break;
                        case 7:
                            DisplayHistory("Execution History:");
                            break;
                        case 8:
                            ExecuteTasks();
                            break;
                        case 9:
                            ReturnTaskToPool();
                            break;
                        case 10:
                            GetTaskFromPool();
                            break;
                        case 11:
                            Console.WriteLine("Exiting the program.");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                }
            }
        }

        static void ExecuteNextTask()
        {
            taskScheduler.ExecuteNext(task =>
            {
                Console.WriteLine($"Executing task: {task}");
                executionHistory.Add($"Executed task: {task}");
            });
        }

        static void ExecuteAllTasks()
        {
            taskScheduler.ExecuteAll(task =>
            {
                Console.WriteLine($"Executing task: {task}");
                executionHistory.Add($"Executed task: {task}");
            });
        }

        static void ClearQueueAndResetPriorities()
        {
            Console.WriteLine("Clearing queue and resetting priorities...");
            taskScheduler.ClearQueueAndResetPriorities();
            Console.WriteLine("Queue and priorities have been cleared and reset.");
        }

        static void ClearQueue()
        {
            Console.WriteLine("Clearing queue...");
            taskScheduler.ClearQueue();
            Console.WriteLine("Queue has been cleared.");
        }

        static void ResetPriorities()
        {
            Console.WriteLine("Resetting priorities...");
            taskScheduler.ResetPriorities();
            Console.WriteLine("Priorities have been reset.");
        }

        static void DisplayHistory(string title)
        {
            Console.WriteLine(title);
            foreach (var entry in executionHistory)
            {
                Console.WriteLine(entry);
            }
        }

        static void ExecuteTasks()
        {
            while (taskScheduler.HasTasks())
            {
                ExecuteNextTask();
            }
        }

        static void ReturnTaskToPool()
        {
            Console.WriteLine("Returning task to the pool...");

            var task = taskScheduler.InitializeTask();

            taskScheduler.ReturnTaskToPool(task);

            Console.WriteLine("Task has been returned to the pool.");
        }

        static void GetTaskFromPool()
        {
            var task = taskScheduler.GetTaskFromPool();

            Console.WriteLine("Got task from the pool:");
            Console.WriteLine(task);

            Console.WriteLine($"Executing task: {task}");
            executionHistory.Add($"Executed task: {task}");
        }
    }
}