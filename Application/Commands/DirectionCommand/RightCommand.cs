namespace Game;

[Show("Вправо", Symbols = ['\u2192', 'D'], OrderPriority = 3)]
public class RightCommand : KeyCommand, IDirection
{
    public Point Direction => new Point(0, 1);

    public RightCommand() : base(ConsoleKey.D, ConsoleKey.RightArrow)
    {
    }
}