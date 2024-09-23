using Game;
using Models;
using Models.Fabric;

namespace UI;

static class GameExecutor
{
    private static MazeWriter mazeWriter = null!;
    private static IMazeFormatter[] formatters = null!;
    private static IMaze maze = null!;
    private static Player2[] players = null!;
    private static readonly Difficulty[] difficulties = [new Easy(), new Medium(), new Hard(), new MADNESS()];
    private static Difficulty difficulty = null!;
    private static MazeBuilder[] mazeBuilders = null!;
    private static GameManager gameManager = null!;

    public static void Start(IUIArtist artist)
    {
        // Artist.Draw(new Menu());

        gameManager = GameManager.GetManager();
        SetGameOptions();
        StartGame();
        Artist.Draw(new WriterToDrawingAdapter(mazeWriter));
        Console.ReadKey();
    }

    private static void StartGame()
    {
        var player = ChoosePlayer();
        gameManager.Player2 = player;
        gameManager.Start();
    }

    private static Player2 ChoosePlayer()
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

    private static Player2 GetPlayer()
    {
        return ConsoleHelper.FindNamingElementByInput(players, "Выберите персонажа из списка выше");
    }

    private static void SetGameOptions()
    {
        PrintOffer("Выберите уровень сложности: ");
        Print(difficulties);
        mazeBuilders = GetMazeBuilders();
        difficulty = GetDifficulty();
        var mazeBuilder = GetMazeBuilder();
        maze = gameManager.CreateMaze(mazeBuilder);
        formatters = GetFormatters();
        PrintOffer("Выберите способ вывода лабиринта: ");
        PrintFormatters();
        var formatter = GetMazeFormatter();
        mazeWriter = new ConsoleMazeWriter(maze, Console.Out, formatter);
    }

    private static MazeBuilder GetMazeBuilder()
    {
        var name = difficulty.Name;
        return mazeBuilders.FirstOrDefault(x => x.Name == name)!;
    }

    private static MazeBuilder[] GetMazeBuilders()
    {
        return [new EasyMazeBuilder(), new MediumMazeBuilder(), new HardMazeBuilder(), new MadnessMazeBuilder()];
    }

    private static IMazeFormatter[] GetFormatters()
    {
        return [new DefaultMazeFormatter(), new WeirdMazeFormatter()];
    }

    private static IMazeFormatter GetMazeFormatter()
    {
        return ConsoleHelper.FindNamingElementByInput(formatters, "Выберите способ вывода из списка выше");
    }

    private static void PrintFormatters()
    {
        foreach (var format in formatters)
        {
            var newSymbols = format.Symbols.Select(x => $"'{x}'");
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

    private static Player2[] GetPlayers()
    {
        var ratio = difficulty.SkillRatio;
        return
        [
            new Berserker(maze, (int)(Berserker.BreakableWallsCount * ratio)),
            new Mage(maze, (int)(Mage.HintsMovesCount * ratio)),
            new Tracer(maze, (int)(Tracer.MaxTraces * ratio)),
        ];
    }
}