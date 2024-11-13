using Extensions;
using Models.Fabric;
using Models.Player;
using Ninject;

// ReSharper disable PossibleMultipleEnumeration

namespace UI;

public class GameInitializer
{
    private const ConsoleColor inputColor = ConsoleColor.Green;
    private const ConsoleColor offerColor = ConsoleColor.White;
    private const ConsoleColor optionsColor = ConsoleColor.Yellow;
    public IKernel Kernel { get; }
    private static GameInitializer? instance;

    private GameInitializer(IKernel kernel)
    {
        Kernel = kernel;
    }

    public static GameInitializer GetInstance(IKernel kernel) => instance ??= new GameInitializer(kernel);

    public void Start()
    {
        var difficulties = Kernel.GetAll<Difficulty>();
        var factories = Kernel.GetAll<MazeFactory>();
        var difficulty = PrintAndGetElement(difficulties, GetNamingElementInfo, "Выберите уровень сложности:",
            "Выберите сложность из списка выше");
        Kernel.RebindToConstant(difficulty);
        var factory = PrintAndGetElement(factories, GetNamingElementInfo, "Выберите тип лабиринта:",
            "Выберите тип лабиринта из списка выше");
        Kernel.RebindToConstant(factory);
        Kernel.BindAllBaseClassesFromTo<MazeBuilder, MazeBuilder>();
        var mazeBuilder = GetMazeBuilder(Kernel.GetAll<MazeBuilder>());
        Kernel.RebindToConstant(mazeBuilder);
        Kernel.Bind<IMaze>()
            .ToConstant(Kernel.Get<MazeBuilder>().Maze);
        BindPlayers();
        var players = Kernel.GetAll<PlayerRole>();
        var player = PrintAndGetElement(players, GetNamingElementInfo, "Выберите персонажа:",
            "Выберите персонажа из списка выше");
        Kernel.RebindToConstant(player);
        ConsoleHelper.PrintOffer("Выберите способ вывода лабиринта:", offerColor);
        var mazeFormatters = Kernel.GetAll<MazeFormatter>();
        ConsoleHelper.PrintOptionsSeparately(mazeFormatters, optionsColor, GetFormatterInfo);
        var formatter = GetElement(mazeFormatters, "Выберите способ вывода из списка выше");
        Kernel.RebindToConstant(formatter);
    }

    private static T PrintAndGetElement<T>(IEnumerable<T> options, Func<ShowAttribute, string> action,
        string offerMsg,
        string errorMessage)
    {
        ConsoleHelper.PrintOffer(offerMsg, offerColor);
        ConsoleHelper.PrintOptionsInLine(options, optionsColor, action, " | ");
        return GetElement(options, errorMessage);
    }

    public static string GetNamingElementInfo(ShowAttribute attribute) => attribute.Name;

    private MazeBuilder GetMazeBuilder(IEnumerable<MazeBuilder> builders)
    {
        return builders.FirstOrDefault(x => x.Name == Kernel.Get<Difficulty>().GetShowAttribute()!.Name)!;
    }

    private static T GetElement<T>(IEnumerable<T> collection, string errorMsg) =>
        ConsoleHelper.FindNamingElementByInput(collection, errorMsg, inputColor);

    private static string GetFormatterInfo(ShowAttribute attribute)
    {
        var symbolView = attribute.Symbols!.Select(x => $"'{x}'");
        return $"{attribute.Name}: {string.Join(" | ", symbolView)}";
    }

    private void BindPlayers()
    {
        var ratio = Kernel.Get<Difficulty>().SkillRatio;
        var maze = Kernel.Get<IMaze>();
        Kernel.BindToConstant<PlayerRole>
        (
            new Berserker(maze, ratio, TimeSpan.FromSeconds(1)),
            new Tracer(maze, ratio, TimeSpan.FromSeconds(2), 10)
        );
    }
}