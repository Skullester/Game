namespace Game;

public class UpCommand : MoveCommand
{
    public override string Name => "Вверх";

    public UpCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, '\u2191', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.W);
        keyMap.Add(ConsoleKey.UpArrow);
    }

    protected override Point GetDirection() => new Point(-1, 0);
}