namespace Game;

public class DownCommand : MoveCommand
{
    public override string Name => "Вниз";

    public DownCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, '\u2193', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.S);
        keyMap.Add(ConsoleKey.DownArrow);
    }

    protected override Point GetDirection() => new Point(1, 0);
}