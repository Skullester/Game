namespace Game;

public class DownCommand : DirectionCommand
{
    public override string Name => "Вниз";

    public DownCommand(MoveCommand moveCommand) : base('\u2193', moveCommand)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.S);
        keyMap.Add(ConsoleKey.DownArrow);
    }

    protected override Point GetDirection() => new Point(1, 0);
}