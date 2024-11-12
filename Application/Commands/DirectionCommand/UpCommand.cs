namespace Game;

[Show("Вверх", '\u2191', OrderPriority = 0)]
public class UpCommand : Command, IDirection
{
    public Point Direction => new Point(-1, 0);

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.W);
        keyMap.Add(ConsoleKey.UpArrow);
    }
}