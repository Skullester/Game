namespace ConsoleApp1;

public class Program
{
    private static void Main()
    {
        Console.Write(1);
        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        Console.WriteLine(2);
        // Artist.DrawMenu();
        new GameExecutor().Start();
    }
}