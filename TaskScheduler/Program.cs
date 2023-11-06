using System;
using Task_4;

namespace TaskSchedulerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем экземпляр TaskScheduler с необходимыми делегатами
            var taskScheduler = new TaskScheduler<string, int>(
                task => task.Length, // Функция для определения приоритета задания (длина строки)
                () => Console.ReadLine(), // Функция для инициализации задания
                task => { /* Действие с заданием при сбросе */ });

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Add task");
                Console.WriteLine("2 - Execute next task");
                Console.WriteLine("3 - Exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            taskScheduler.AddTaskFromConsole();
                            break;
                        case 2:
                            taskScheduler.ExecuteNext(task => Console.WriteLine($"Executing task: {task}"));
                            break;
                        case 3:
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
    }
}