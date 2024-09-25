using Game;
using Models;
using Models.Fabric;
using Patterns.Naming;

namespace UI;

public static class GameExecutor
{
    private static ConsoleMazeWriter mazeWriter = null!;
    private static readonly MazeFormatter[] formatters = [new DefaultMazeFormatter(), new WeirdMazeFormatter()];
    private static IMaze? maze = null!;
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
        gameManager.GameArtist = GameArtist.GetArtist(mazeWriter);
        gameManager.Start();
    }

    private static void SetGameOptions()
    {
        PrintOffer("Выберите уровень сложности: ");
        Print(difficulties);
        difficulty = GetDifficulty();
        PrintOffer("Выберите тип лабиринта: ");
        Print(factories);
        var factory = GetFactory();
        mazeBuilders = GetMazeBuilders(factory);
        var mazeBuilder = GetMazeBuilder();
        maze = mazeBuilder.Maze;
        var player = ChoosePlayer();
        gameManager.Player = player;
        maze = gameManager.CreateMaze(mazeBuilder);
        PrintOffer("Выберите способ вывода лабиринта: ");
        PrintFormatters();
        var formatter = GetMazeFormatter();
        mazeWriter = new ConsoleMazeWriter(maze, formatter, 1);
    }

    private static MazeFactory GetFactory()
    {
        return ConsoleHelper.FindNamingElementByInput(factories, "Выберите тип лабиринта из списка выше");
    }

    private static Player ChoosePlayer()
    {
        PrintOffer("Выберите персонажа: ");
        players = GetPlayers();
        Print(players);
        return GetPlayer();
    }

    private static Difficulty GetDifficulty()
    {
        return ConsoleHelper.FindNamingElementByInput(difficulties, "Выберите сложность из списка выше");
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

    private static Player GetPlayer()
    {
        return ConsoleHelper.FindNamingElementByInput(players, "Выберите персонажа из списка выше");
    }

    private static MazeBuilder GetMazeBuilder() => mazeBuilders!.FirstOrDefault(x => x.Name == difficulty.Name)!;


    private static MazeFormatter GetMazeFormatter()
    {
        return ConsoleHelper.FindNamingElementByInput(formatters, "Выберите способ вывода из списка выше");
    }

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