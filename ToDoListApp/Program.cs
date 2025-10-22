// See https://aka.ms/new-console-template for more information

using System.Reflection.Metadata;
using System.Text.Json;

class Program
{
    static void Main()
    {

        var tasks = new List<string>();

        string filePath = "tasks.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            tasks = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }

        Console.WriteLine("To-Do List Application");
        Console.WriteLine("----------------------");

        Console.WriteLine("Select an operation (1, 2, 3 or 4 and press Enter): ");

        Console.WriteLine("1. Add Task");
        Console.WriteLine("2. Remove Task");
        Console.WriteLine("3. Display All Tasks");
        Console.WriteLine("4. Clear Task List");
        Console.WriteLine("Choice: \n");

        while (true)
        {
            string? rawChoice = Console.ReadLine();
            if (!int.TryParse(rawChoice, out int initialChoice))
            {
                Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 4.");
                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey();
                Todos.ClearBelowMenu(8);
                continue;
            }

            switch (initialChoice)
            {
                case 1:
                    Console.WriteLine("\nPlease enter a task: ");
                    string? userAddChoice = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(userAddChoice))
                    {
                        tasks.Add(userAddChoice);
                        File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
                        Console.WriteLine("\nTask added.");
                    }
                    else
                    {
                        Console.WriteLine("\nEmpty task not added.");
                    }
                    break;

                case 2:
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("\nNo tasks to remove, list is empty.");
                        break;
                    }
                    Console.WriteLine("\nCurrent tasks: ");
                    Todos.DisplayAll(tasks);

                    Console.WriteLine("\nEnter the number of the task to remove:");
                    string? userRemoveString = Console.ReadLine();

                    if (!int.TryParse(userRemoveString, out int userRemoveChoice))
                    {
                        Console.WriteLine("\nInvalid input. Please enter a valid number.");
                    }
                    else
                    {
                        int indexToRemove = userRemoveChoice - 1; // convert to 0-based index

                        if (indexToRemove < 0 || indexToRemove >= tasks.Count)
                        {
                            Console.WriteLine("That task number does not exist.");
                        }
                        else
                        {
                            string removed = tasks[indexToRemove];
                            tasks.RemoveAt(indexToRemove);
                            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
                            Console.WriteLine($"Removed task: {removed}");
                        }
                    }
                    break;

                case 3:
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("No tasks in the list.");
                    }
                    else
                    {
                        Console.WriteLine("Task list: ");
                        Todos.DisplayAll(tasks);
                    }
                    break;

                case 4:
                    if (tasks.Count > 0)
                    {
                        tasks.Clear();
                        File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
                        Console.WriteLine("Task list cleared.");
                    }
                    else
                    {
                        Console.WriteLine("Task list empty, no clearing required.");
                    }
                    break;
            }
            Console.WriteLine("\nReturn to main selection? (y/n)");

            while (true)
            {
                string? returnChoice = Console.ReadLine().ToLower();

                if (returnChoice == "y")
                {
                    Todos.ClearBelowMenu(8);
                    break;
                }
                else if (returnChoice == "n")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Choose key y or n.");
                }
            }

        }
    }
}
