using Game;
using Models;
using Models.Fabric;
using Patterns.Naming;

namespace UI;

public static class GameExecutor
{
    private static ConsoleMazeWriter mazeWriter = null!;
    private static readonly MazeFormatter[] formatters = [new DefaultMazeFormatter(), new WeirdMazeFormatter()];
    private static IMaze? maze;
    private static Player[] players = null!;
    private static readonly Difficulty[] difficulties = [new Easy(), new Medium(), new Hard(), new MADNESS()];
    private static Difficulty difficulty = null!;
    private static readonly MazeFactory[] factories = [new MazeFactoryDefault(), new MazeFactoryFire()];
    private static MazeBuilder[]? mazeBuilders;

    private static GameManager gameManager = null!;

    public static void Start()
    {
        gameManager = GameManager.GetManager();
        SetGameOptions();
        gameManager.Artist = GameArtist.GetArtist(mazeWriter);
        gameManager.Start();
    }

    private static T PrintAndGetElement<T>(IEnumerable<T> collection, string offerMsg, string errorMsg)
        where T : INaming
    {
        PrintOffer(offerMsg);
        Print(collection);
        return GetElement(collection, errorMsg);
    }

    private static void SetGameOptions()
    {
        difficulty = PrintAndGetElement(difficulties, "Выберите уровень сложности:",
            "Выберите сложность из списка выше");
        var factory = PrintAndGetElement(factories, "Выберите тип лабиринта:", "Выберите тип лабиринта из списка выше");
        mazeBuilders = GetMazeBuilders(factory);
        var mazeBuilder = GetMazeBuilder();
        maze = mazeBuilder.Maze;
        players = GetPlayers();
        var player = PrintAndGetElement(players, "Выберите персонажа:", "Выберите персонажа из списка выше");
        gameManager.Player = player;
        gameManager.CreateMaze(mazeBuilder);
        PrintOffer("Выберите способ вывода лабиринта: ");
        PrintFormatters();
        var formatter = GetElement(formatters, "Выберите способ вывода из списка выше");
        mazeWriter = new ConsoleMazeWriter(maze, formatter);
    }

    private static MazeBuilder[] GetMazeBuilders(MazeFactory factory)
    {
        return
        [
            new EasyMazeBuilder(factory),
            new MediumMazeBuilder(factory),
            new HardMazeBuilder(factory),
            new MadnessMazeBuilder(factory)
        ];
    }

    private static MazeBuilder GetMazeBuilder() => mazeBuilders!.FirstOrDefault(x => x.Name == difficulty.Name)!;

    private static T GetElement<T>(IEnumerable<T> collection, string errorMsg) where T : INaming =>
        ConsoleHelper.FindNamingElementByInput(collection, errorMsg);

    private static void PrintFormatters()
    {
        foreach (var format in formatters)
        {
            var newSymbols = format.Symbols.Select(x => $"'{x.Value}'");
            var symbols = string.Join(" | ", newSymbols);
            ConsoleHelper.PrintLine($"{format.Name}: {symbols}");
        }
    }

    private static void PrintOffer(string message)
    {
        ConsoleHelper.PrintLineWithColor(message, ConsoleColor.White);
        ConsoleHelper.SetConsoleColor(ConsoleColor.Yellow);
    }

    private static void Print<T>(IEnumerable<T> collection) where T : INaming
    {
        var names = collection.Select(x => x.Name);
        ConsoleHelper.PrintLine(string.Join(" | ", names));
    }

    private static Player[] GetPlayers()
    {
        var ratio = difficulty.SkillRatio;
        return
        [
            new Berserker(maze, (int)(Berserker.BreakableWallsConst * ratio)),
            new Mage(maze, (int)(Mage.HintsMovesCount * ratio)),
            new Tracer(maze, (int)(Tracer.TracesConst * ratio)),
        ];
    }
}