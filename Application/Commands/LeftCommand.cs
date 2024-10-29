namespace Game;

public class LeftCommand : DirectionCommand
{
    public override string Name => "Влево";

    public LeftCommand(MoveCommand moveCommand) : base('\u2190', moveCommand)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.A);
        keyMap.Add(ConsoleKey.LeftArrow);
    }

    protected override Point GetDirection() => new Point(0, -1);
}