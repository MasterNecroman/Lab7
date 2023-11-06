using System;

namespace task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Calculator<int> intCalculator = new Calculator<int>((a, b) => a + b, (a, b) => a - b, (a, b) => a * b, (a, b) => a / b);
                Calculator<double> doubleCalculator = new Calculator<double>((a, b) => a + b, (a, b) => a - b, (a, b) => a * b, (a, b) => a / b);

                Console.WriteLine("Choose a data type:");
                Console.WriteLine("1 - int");
                Console.WriteLine("2 - double");
                Console.WriteLine("3 - Exit");

                if (int.TryParse(Console.ReadLine(), out int choice) && (choice == 1 || choice == 2))
                {
                    if (choice == 1)
                    {
                        PerformOperations(intCalculator);
                    }
                    else if (choice == 2)
                    {
                        PerformOperations(doubleCalculator);
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Exiting the program.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }

        static void PerformOperations<T>(Calculator<T> calculator)
        {
            while (true)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1 - Addition");
                Console.WriteLine("2 - Subtraction");
                Console.WriteLine("3 - Multiplication");
                Console.WriteLine("4 - Division");
                Console.WriteLine("5 - Back");

                if (int.TryParse(Console.ReadLine(), out int operationChoice))
                {
                    if (operationChoice >= 1 && operationChoice <= 4)
                    {
                        Console.Write("Enter the first number: ");
                        if (TryParseInput(out T num1))
                        {
                            Console.Write("Enter the second number: ");
                            if (TryParseInput(out T num2))
                            {
                                T result = default;

                                switch (operationChoice)
                                {
                                    case 1:
                                        result = calculator.PerformOperation(num1, num2, calculator.Addition);
                                        break;
                                    case 2:
                                        result = calculator.PerformOperation(num1, num2, calculator.Subtraction);
                                        break;
                                    case 3:
                                        result = calculator.PerformOperation(num1, num2, calculator.Multiplication);
                                        break;
                                    case 4:
                                        result = calculator.PerformOperation(num1, num2, calculator.Division);
                                        break;
                                }

                                Console.WriteLine($"Result: {result}");
                            }
                        }
                    }
                    else if (operationChoice == 5)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
            }
        }

        static bool TryParseInput<T>(out T value)
        {
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(Console.ReadLine(), out int intValue))
                {
                    value = (T)Convert.ChangeType(intValue, typeof(T));
                    return true;
                }
            }
            else if (typeof(T) == typeof(double))
            {
                if (double.TryParse(Console.ReadLine(), out double doubleValue))
                {
                    value = (T)Convert.ChangeType(doubleValue, typeof(T));
                    return true;
                }
            }

            Console.WriteLine("Invalid input. Please enter a valid number.");
            value = default;
            return false;
        }
    }
}