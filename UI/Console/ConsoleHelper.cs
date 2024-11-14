using Extensions;

// ReSharper disable PossibleMultipleEnumeration

namespace Game;

public static class ConsoleHelper
{
    private const string lackOfShowAttributeMessage = """Атрибут "Show" отсутствует""";

    public static class CursorPositionContainer
    {
        private static int left;
        private static int top;

        public static void Save()
        {
            left = Console.CursorLeft;
            top = Console.CursorTop;
        }

        public static (int, int) Get() => (left, top);

        public static void Set()
        {
            Console.SetCursorPosition(left, top);
        }
    }

    public static void SetColor(ConsoleColor color) => Console.ForegroundColor = color;

    public static void PrintLineWithColor(string text, ConsoleColor newColor, bool saveOldColor = true)
    {
        PrintWithColor(text + Environment.NewLine, newColor, saveOldColor);
    }

    public static void PrintWithColor<T>(T text, ConsoleColor newColor, bool saveOldColor = true)
    {
        var color = Console.ForegroundColor;
        SetColor(newColor);
        Print(text);
        if (saveOldColor)
            SetColor(color);
    }

    public static void PrintLine<T>(T text)
    {
        Print(text + Environment.NewLine);
    }

    private static void Print<T>(T text)
    {
        Console.Write(text);
    }

    public static T FindShownElementByInput<T>(IEnumerable<T> collection, string errorMessage, ConsoleColor inputColor)
    {
        VerifyShowAttribute(collection.First());
        Console.ForegroundColor = inputColor;
        var valueAttributeTuple = collection.Select(x => (Value: x, Attribute: x.GetShowAttribute()!));
        T element;
        bool isError;
        do
        {
            var input = Console.ReadLine()!;
            // ReSharper disable once PossibleMultipleEnumeration
            element = valueAttributeTuple
                .FirstOrDefault(x => x.Attribute.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase)).Value;
            isError = element is null || string.IsNullOrEmpty(input);
            if (isError) PrintError(errorMessage);
        } while (isError);

        return element;
    }

    private static void VerifyShowAttribute<T>(T value)
    {
        if (!AttributeEx.HasShowAttribute(value))
            throw new ArgumentException(lackOfShowAttributeMessage);
    }

    public static void PrintOffer(string message, ConsoleColor offerColor)
    {
        PrintLineWithColor(message, offerColor);
    }

    public static void PrintError(string text) => PrintLineWithColor(text, ConsoleColor.Red);

    public static void PrintOptionsInLine<T>(IEnumerable<T> options, ConsoleColor optionsColor,
        Func<ShowAttribute, string> action,
        string separator)
    {
        var strings = GetOptionsString(options, optionsColor, action);
        var join = string.Join(separator, strings);
        PrintLine(join);
    }

    private static IEnumerable<string> GetOptionsString<T>(IEnumerable<T> options, ConsoleColor optionsColor,
        Func<ShowAttribute, string> action)
    {
        SetColor(optionsColor);
        var showAttributes = options.GetShowAttributeElementsFrom(true);
        return showAttributes.Select(action);
    }

    public static void PrintOptionsSeparately<T>(IEnumerable<T> options, ConsoleColor optionsColor,
        Func<ShowAttribute, string> action)
    {
        var strings = GetOptionsString(options, optionsColor, action);
        foreach (var se in strings)
        {
            PrintLine(se);
        }
    }
}