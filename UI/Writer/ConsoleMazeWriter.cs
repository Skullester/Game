using Models;

namespace UI.Displaying;

[Show("Консоль")]
public class ConsoleMazeWriter : MazeWriter
{
    public ConsoleMazeWriter(IMaze maze, MazeFormatter formatter, int delay = 0) : base(maze, Console.Out,
        formatter, delay)
    {
    }

    protected override void Write(char sym, IMazeElement el)
    {
        if (el is IColorable colorable)
            ConsoleHelper.SetColor(colorable.Color);
        base.Write(sym, el);
    }
}