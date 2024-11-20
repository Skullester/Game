namespace Game;

[Show("Вверх", Symbols = ['\u2191'], OrderPriority = 0)]
public class UpCommand : KeyCommand, IDirection
{
    public Point Direction => new Point(-1, 0);

    public UpCommand() : base(ConsoleKey.W, ConsoleKey.UpArrow)
    {
    }
}