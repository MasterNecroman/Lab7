using System;

namespace task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository<string> repository = new Repository<string>();

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Add item");
                Console.WriteLine("2 - Search items");
                Console.WriteLine("3 - List all items");
                Console.WriteLine("4 - Remove item");
                Console.WriteLine("5 - Clear all items");
                Console.WriteLine("6 - Exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter an item: ");
                            string item = Console.ReadLine();
                            repository.Add(item);
                            break;
                        case 2:
                            Console.WriteLine("Enter a search criteria:");
                            string searchCriteria = Console.ReadLine();
                            var foundItems = repository.Find(x => x.Contains(searchCriteria));
                            Console.WriteLine("Matching items:");
                            foreach (var foundItem in foundItems)
                            {
                                Console.WriteLine(foundItem);
                            }
                            break;
                        case 3:
                            Console.WriteLine("All items in the repository:");
                            foreach (var storedItem in repository.GetAll())
                            {
                                Console.WriteLine(storedItem);
                            }
                            break;
                        case 4:
                            Console.Write("Enter the item to remove: ");
                            string itemToRemove = Console.ReadLine();
                            if (repository.Remove(itemToRemove))
                            {
                                Console.WriteLine("Item removed successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Item not found in the repository.");
                            }
                            break;
                        case 5:
                            repository.Clear();
                            Console.WriteLine("All items cleared from the repository.");
                            break;
                        case 6:
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