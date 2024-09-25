using Models;

namespace UI;

public class ConsoleMazeWriter : MazeWriter
{
    public override string Name => "Консоль";

    public ConsoleMazeWriter(IMaze? maze, MazeFormatter formatter, int delay = 0) : base(maze, Console.Out,
        formatter, delay)
    {
    }

    protected override void Write(char sym)
    {
        writer.Write(sym);
    }
}