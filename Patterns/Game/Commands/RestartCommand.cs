using Models;

namespace Game;

public class RestartCommand : Command
{
    public override string Name => "Перезапуск";

    public RestartCommand(IMaze maze) : base(maze, 'R')
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.R);
    }

    public override void Execute()
    {
        manager.State = GameState.Reset;
    }
}