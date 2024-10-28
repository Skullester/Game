using Infrastructure;
using Models.Fabric;
using Models.Player;
using Ninject;
using Ninject.Extensions.Conventions;

// ReSharper disable PossibleMultipleEnumeration

namespace UI;

public class GameInitializer
{
    public IKernel Kernel { get; }
    private static GameInitializer? instance;

    private GameInitializer(IKernel kernel)
    {
        Kernel = kernel;
    }

    public static GameInitializer GetInstance(IKernel kernel) => instance ??= new GameInitializer(kernel);

    public void Start()
    {
        SetGameOptions();
    }

    private void SetGameOptions()
    {
        var difficulties = Kernel.GetAll<Difficulty>();
        var factories = Kernel.GetAll<MazeFactory>();
        var difficulty = PrintAndGetElement(difficulties, "Выберите уровень сложности:",
            "Выберите сложность из списка выше");
        Kernel.RebindToConstant(difficulty);
        var factory = PrintAndGetElement(factories, "Выберите тип лабиринта:", "Выберите тип лабиринта из списка выше");
        Kernel.RebindToConstant(factory);
        Kernel.BindAllBaseClassesFromTo<MazeBuilder, MazeBuilder>();
        var mazeBuilder = GetMazeBuilder(Kernel.GetAll<MazeBuilder>());
        Kernel.RebindToConstant(mazeBuilder);
        Kernel.Bind<IMaze>()
            .ToConstant(Kernel.Get<MazeBuilder>().Maze);
        BindPlayers();
        var players = Kernel.GetAll<Player>();
        var player = PrintAndGetElement(players, "Выберите персонажа:", "Выберите персонажа из списка выше");
        Kernel.RebindToConstant(player);
        ConsoleHelper.PrintOffer("Выберите способ вывода лабиринта: ");
        PrintFormatters();
        var formatter = GetElement(Kernel.GetAll<MazeFormatter>(), "Выберите способ вывода из списка выше");
        Kernel.RebindToConstant(formatter);
    }

    private static T PrintAndGetElement<T>(IEnumerable<T> collection, string offerMsg, string errorMsg)
        where T : INaming
    {
        ConsoleHelper.PrintOffer(offerMsg);
        ConsoleHelper.Print(collection);
        return GetElement(collection, errorMsg);
    }

    private MazeBuilder GetMazeBuilder(IEnumerable<MazeBuilder> builders) =>
        builders.FirstOrDefault(x => x.Name == Kernel.Get<Difficulty>().Name)!;

    private static T GetElement<T>(IEnumerable<T> collection, string errorMsg) where T : INaming =>
        ConsoleHelper.FindNamingElementByInput(collection, errorMsg);

    private void PrintFormatters()
    {
        foreach (var format in Kernel.GetAll<MazeFormatter>())
        {
            var newSymbols = format.Symbols.Select(x => $"'{x.Value}'");
            var symbols = string.Join(" | ", newSymbols);
            ConsoleHelper.PrintLine($"{format.Name}: {symbols}");
        }
    }

    private void BindPlayers()
    {
        var ratio = Kernel.Get<Difficulty>().SkillRatio;
        var maze = Kernel.Get<IMaze>();
        Kernel.BindToConstant<Player>(
            new Berserker(maze, ratio, TimeSpan.FromSeconds(1)),
            new Tracer(maze, ratio, TimeSpan.FromSeconds(2)),
            new Mage(maze, ratio, TimeSpan.FromSeconds(2))
        );
    }
}