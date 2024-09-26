using Game;
using Models.Fabric;
using Models.Maze;
using Models.Naming;
using Models.Player;
using UI.Displaying;

namespace UI;

public static class GameInitializer
{
    public static ConsoleMazeWriter MazeWriter = null!;
    public static GameManager GameManager = null!;
    public static Command[] Commands = null!;
    private static readonly MazeFormatter[] formatters = [new DefaultMazeFormatter(), new WeirdMazeFormatter()];
    private static IMaze maze = null!;
    private static Player[] players = null!;
    private static readonly Difficulty[] difficulties = [new Easy(), new Medium(), new Hard(), new MADNESS()];
    private static Difficulty difficulty = null!;
    private static readonly MazeFactory[] factories = [new MazeFactoryDefault(), new MazeFactoryFire()];
    private static MazeBuilder[]? mazeBuilders;


    public static void Start()
    {
        SetGameOptions();
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
        PrintOffer("Выберите способ вывода лабиринта: ");
        PrintFormatters();
        var formatter = GetElement(formatters, "Выберите способ вывода из списка выше");
        MazeWriter = new ConsoleMazeWriter(maze, formatter);
        GameManager = GameManager.GetManager(player, mazeBuilder);
        Commands = GetCommands(player, GameManager);
    }

    private static T PrintAndGetElement<T>(IEnumerable<T> collection, string offerMsg, string errorMsg)
        where T : INaming
    {
        PrintOffer(offerMsg);
        Print(collection);
        return GetElement(collection, errorMsg);
    }

    private static Command[] GetCommands(Player player, IGameManager gameManager) =>
    [
        new LeftCommand(maze, gameManager, player),
        new RightCommand(maze, gameManager, player),
        new UpCommand(maze, gameManager, player),
        new DownCommand(maze, gameManager, player),
        new SkillCommand(maze, gameManager, player),
        new RestartCommand(maze, gameManager, player)
    ];

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
            new Mage(maze, (int)(Mage.HintsConst * ratio)),
            new Tracer(maze, (int)(Tracer.TracesConst * ratio)),
        ];
    }
}