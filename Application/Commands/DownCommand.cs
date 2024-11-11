namespace Game;

[Show("Вниз", '\u2193', Priority = 1)]
public class DownCommand : Command, IDirection
{
    public Point Direction => new Point(1, 0);

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.S);
        keyMap.Add(ConsoleKey.DownArrow);
    }
}