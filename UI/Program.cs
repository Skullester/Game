using Extensions;
using Models.Fabric;
using Ninject;
using Ninject.Modules;
using UI.Artist;

namespace UI;

public class Program
{
    private const int GameTries = 5;

    private class GameModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.BindAllBaseClassesFromTo<Difficulty, Difficulty>();
            Kernel.BindAllInterfacesFromTo<IController, IController>();
            Kernel.BindAllBaseClassesTo<MazeFormatter>();
            Kernel.BindAllBaseClassesFromTo<MazeFactory, MazeFactory>();
            Kernel.BindAllBaseClassesFromTo<KeyCommand, KeyCommand>();
            Kernel.BindAllInterfacesFromTo<ICommand, ICommand>();
            Kernel!.Bind<GameInitializer>()
                .ToConstant(GameInitializer.GetInstance(Kernel));
        }
    }

    private static void Main()
    {
        var kernel = GetKernel();
        kernel.Get<IGameArtist>().Initialize();
    }

    private static IKernel GetKernel()
    {
        var kernel = new StandardKernel(new GameModule());

        kernel.Get<GameInitializer>()
            .Start();
        kernel.Bind<IGameArtist>()
            .To<ConsoleGameArtist>()
            .InSingletonScope();
        kernel.Bind<IGameManager>()
            .To<GameManager>()
            .InSingletonScope();
        kernel.Bind<MazeWriter>()
            .To<ConsoleMazeWriter>()
            .InSingletonScope();
        kernel.Bind<int>().ToConstant(GameTries).WhenInjectedInto<IGameManager>();
        kernel.Rebind<IInteractablePlayerCommand>().ToConstant(kernel.GetAll<ICommand>().OfType<MoveCommand>().First());
        return kernel;
    }
}