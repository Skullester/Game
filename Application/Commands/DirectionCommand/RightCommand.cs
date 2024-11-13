namespace Game;

[Show("Вправо", Symbols= ['\u2192'], OrderPriority = 3)]
public class RightCommand : Command, IDirection
{
    public Point Direction => new Point(0, 1);

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.D);
        keyMap.Add(ConsoleKey.RightArrow);
    }
}