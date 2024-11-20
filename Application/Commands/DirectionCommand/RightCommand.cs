namespace Game;

[Show("Вправо", Symbols = ['\u2192'], OrderPriority = 3)]
public class RightCommand : KeyCommand, IDirection
{
    public Point Direction => new Point(0, 1);

    public RightCommand() : base(ConsoleKey.D, ConsoleKey.RightArrow)
    {
    }

    protected void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.D);
        keyMap.Add(ConsoleKey.RightArrow);
    }
}