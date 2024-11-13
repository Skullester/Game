namespace Game;

[Show("Влево", Symbols = ['\u2190'], OrderPriority = 2)]
public class LeftCommand : Command, IDirection
{
    public Point Direction => new Point(0, -1);

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.A);
        keyMap.Add(ConsoleKey.LeftArrow);
    }
}