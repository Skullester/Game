using Models.Fabric;
using Models.Player;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Syntax;
using UI.Artist;

namespace UI;

public class Program
{
    private class GameModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x =>
                x.FromAssemblyContaining<Difficulty>().Select(typeof(Difficulty).IsAssignableFrom)
                    .BindAllBaseClasses());
            Kernel.Bind(x =>
                x.FromThisAssembly().Select(typeof(MazeFormatter).IsAssignableFrom).BindAllBaseClasses());
            Kernel.Bind(x =>
                x.FromAssemblyContaining<MazeFactory>().Select(typeof(MazeFactory).IsAssignableFrom)
                    .BindAllBaseClasses());
            Kernel.Bind(x =>
                x.FromAssemblyContaining<Command>().Select(typeof(Command).IsAssignableFrom)
                    .BindAllBaseClasses());
        }
    }

    private static void Main()
    {
        var kernel = InitializeKernel();
        kernel.Get<IGameArtist>().Initialize();
    }

    private static IKernel InitializeKernel()
    {
        var kernel = new StandardKernel(new GameModule());
        kernel.Bind<GameInitializer>()
            .ToConstant(GameInitializer.GetInstance(kernel));
        kernel.Get<GameInitializer>()
            .Start();
        kernel.Bind<IGameArtist>()
            .To<ConsoleGameArtist>()
            .InSingletonScope();
        kernel.Bind<IGameManager>()
            .ToMethod(x => GameManager.GetManager(x.Kernel.Get<Player>(), x.Kernel.Get<MazeBuilder>()));
        kernel.Bind<MazeWriter>().To<ConsoleMazeWriter>();
        return kernel;
    }
}