using ConsoleApp1.Player.Roles;
using MazePrinter;
using Patterns;

namespace ConsoleApp1;

class GameExecutor
{
    private MazeWriter mazeWriter = null!;
    private IMazeFormatter[] formatters = null!;
    private Player2[] players = null!;
    private Difficulty[] difficulties = null!;
    public void Start()
    {
        GenerateMaze();
        StartGame();
        Console.ReadKey();
    }

    private void StartGame()
    {
        ChoosePlayer();
    }

    private void ChoosePlayer()
    {
        PrintOffer("Выберите персонажа: ");
        Print(players);
    }

    private void GenerateMaze()
    {
        var (height, width) = GetMazeParameters();
        var maze = RectangularMaze.GetMaze(height, width);
        formatters = GetFormatters();
        PrintOffer("Выберите способ вывода лабиринта: ");
        PrintFormatters();
        var formatter = GetMazeFormatter();
        mazeWriter = new ConsoleMazeWriter(maze, Console.Out, formatter);
    }

    private IMazeFormatter[] GetFormatters()
    {
        return [new DefaultMazeFormatter(), new WeirdMazeFormatter()];
    }

    private IMazeFormatter GetMazeFormatter()
    {
        return ConsoleHelper.FindNamingElementByInput(formatters, "Такого способа вывода не существует");
    }

    private void PrintFormatters()
    {
        foreach (var format in formatters)
        {
            var newSymbols = format.Symbols.Select(x => $"'{x}'");
            var symbols = string.Join(" | ", newSymbols);
            ConsoleHelper.PrintLine($"{format.Name}: {symbols}");
        }
    }

    private void PrintOffer(string message)
    {
        ConsoleHelper.PrintLineWithColor(message, ConsoleColor.White);
        ConsoleHelper.SetConsoleColor(ConsoleColor.Yellow);
    }

    private void Print<T>(IEnumerable<T> collection) where T : INaming
    {
        var names = collection.Select(x => x.Name);
        ConsoleHelper.PrintLine(string.Join(" | ", names));
    }

    private Player2[] GetPlayers(IMaze maze)
    {
        return [new Berserker(maze, "сложность"), new Mage(maze,), new Tracer(maze,)];
    }

    private static (int, int) GetMazeParameters()
    {
        var input = InputMazeParameters();
        var sizes = input!.Select(int.Parse)
            .ToArray();
        return sizes.ParseArrayToTuple();
    }

    private static string[]? InputMazeParameters()
    {
        ConsoleHelper.PrintLineWithColor($"Введите высоту и ширину лабиринта: \nПример: 15 5", ConsoleColor.White);
        ConsoleHelper.SetConsoleColor(ConsoleColor.Cyan);
        string[]? input;
        do
        {
            input = Console.ReadLine()?
                .Split();
        } while (CheckInput(input));

        return input;
    }

    private static bool CheckInput(string[]? input)
    {
        var isIncorrect = string.IsNullOrWhiteSpace(input?[0]) || input.Length != 2;
        return ConsoleHelper.CheckError(isIncorrect, "Ошибка! Введите данные по примеру");
    }
}