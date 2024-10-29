namespace Game;

public class RightCommand : DirectionCommand
{
    public override string Name => "Вправо";

    public RightCommand(MoveCommand moveCommand) : base('\u2192', moveCommand)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.D);
        keyMap.Add(ConsoleKey.RightArrow);
    }

    protected override Point GetDirection() => new Point(0, 1);
}