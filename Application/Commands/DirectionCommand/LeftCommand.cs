namespace Game;

[Show("Влево", Symbols = ['\u2190', 'A'], OrderPriority = 2)]
public class LeftCommand : KeyCommand, IDirection
{
    public Point Direction => new Point(0, -1);

    public LeftCommand() : base(ConsoleKey.A, ConsoleKey.LeftArrow)
    {
    }
}