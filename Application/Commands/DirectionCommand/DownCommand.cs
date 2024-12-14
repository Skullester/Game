namespace Game;

[Show("Вниз", Symbols = ['\u2193', 'S'], OrderPriority = 1)]
public class DownCommand : KeyCommand, IDirection
{
    public Point Direction => new Point(1, 0);

    public DownCommand() : base(ConsoleKey.S, ConsoleKey.DownArrow)
    {
    }
}