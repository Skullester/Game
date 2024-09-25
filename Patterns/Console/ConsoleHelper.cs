using System.Drawing;
using Patterns.Naming;

namespace Game;

public static class ConsoleHelper
{
    public static void SetConsoleColor(ConsoleColor color) => Console.ForegroundColor = color;

    public static void PrintLineWithColor(string text, ConsoleColor newColor, bool saveOldColor = true)
    {
        PrintWithColor(text + Environment.NewLine, newColor, saveOldColor);
    }

    public static void PrintWithColor(string text, ConsoleColor newColor, bool saveOldColor = true)
    {
        var color = Console.ForegroundColor;
        SetConsoleColor(newColor);
        Print(text);
        if (saveOldColor)
            SetConsoleColor(color);
    }

    public static void PrintLine(string text)
    {
        Print(text + Environment.NewLine);
    }

    private static void Print(string text)
    {
        Console.Write(text);
    }

    public static T FindNamingElementByInput<T>(IEnumerable<T> collection, string errorMessage)
        where T : INaming
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        T? element;
        bool isError;
        do
        {
            var input = Console.ReadLine()!;
            // ReSharper disable once PossibleMultipleEnumeration
            element = collection
                .FirstOrDefault(x => x.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase));
            isError = element is null || string.IsNullOrWhiteSpace(input);
        } while (CheckError(isError, errorMessage));

        return element!;
    }

    public static bool CheckError(bool condition, string errorMessage)
    {
        if (condition) PrintError(errorMessage);
        return condition;
    }

    public static void PrintError(string text) => PrintLineWithColor(text, ConsoleColor.Red);
}