namespace Game;

[Show("Перезапуск", 'R', OrderPriority = 5)]
public class RestartCommand : Command, IExecutableCommand, IUpdatableCommand
{
    public event Action? Updated;
    private readonly IGameManager gm;

    public RestartCommand(IGameManager gm)
    {
        this.gm = gm;
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.R);
    }

    public void Execute()
    {
        gm.ResetGame();
        Updated?.Invoke();
    }
}