namespace Game;

[Show("Влево", '\u2190', Priority = 2)]
public class LeftCommand : Command, IDirection
{
    public Point Direction => new Point(0, -1);

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.A);
        keyMap.Add(ConsoleKey.LeftArrow);
    }
}