namespace Game;

public class RestartCommand : Command
{
    private readonly IGameManager manager;
    public override string Name => "Перезапуск";

    public RestartCommand(IGameManager manager) : base('R', true)

    {
        this.manager = manager;
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.R);
    }

    public override void Execute()
    {
        manager.State = GameState.Reset;
    }
}