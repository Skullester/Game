namespace Game;

public class RestartCommand : Command
{
    public override string Name => "Перезапуск";

    public RestartCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, 'R', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.R);
    }

    public override bool Execute()
    {
        manager.State = GameState.Reset;
        return false;
    }
}