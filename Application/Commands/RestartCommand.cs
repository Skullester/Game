namespace Game;

[Show("Перезапуск", Symbols = ['R'], OrderPriority = 5)]
public class RestartCommand : KeyCommand, IExecutableCommand, IMapUpdatableCommand
{
    public event Action? Updated;
    private readonly IGameManager gm;

    public RestartCommand(IGameManager gm) : base(ConsoleKey.R)
    {
        this.gm = gm;
    }

    public void Execute()
    {
        gm.ResetGame();
        Updated?.Invoke();
    }
}