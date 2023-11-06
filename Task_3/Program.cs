using System;

namespace task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionCache<string, int> cache = new FunctionCache<string, int>(CalculateStringLength);

            while (true)
            {
                Console.WriteLine("Enter a string (or 'exit' to quit):");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    break;
                }

                int result = cache.GetResult(input);
                Console.WriteLine($"String length: {result}");
            }
        }

        static int CalculateStringLength(string input)
        {
            return input.Length;
        }
    }
}