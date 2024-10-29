namespace Game;

public class UpCommand : DirectionCommand
{
    public override string Name => "Вверх";

    public UpCommand(MoveCommand moveCommand) : base('\u2191', moveCommand)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.W);
        keyMap.Add(ConsoleKey.UpArrow);
    }

    protected override Point GetDirection() => new Point(-1, 0);
}