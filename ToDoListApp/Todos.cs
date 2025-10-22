using System;

class Todos
{
	public List<string> Tasks { get; set; }

	public static void DisplayAll(List<string> tasks)
	{
		for (int i = 0; i < tasks.Count; i++)
		{
			Console.WriteLine($"{i + 1}. {tasks[i]}");
		}
	}

	public static void ClearTaskList(List<string> tasks)
	{
		tasks.Clear();
	}

    public static void ClearBelowMenu(int menuLines)
    {
        // Set cursor position below the menu
        Console.SetCursorPosition(0, menuLines);

        // Clear lines below the menu
        for (int i = menuLines; i < Console.WindowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        // Reset cursor to the menu's end
        Console.SetCursorPosition(0, menuLines);
    }
}
