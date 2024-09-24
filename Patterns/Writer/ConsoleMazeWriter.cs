using Models;

namespace UI;

public class ConsoleMazeWriter : MazeWriter
{
    private readonly double delay;

    public override string Name => "Консоль";

    public ConsoleMazeWriter(IMaze maze, MazeFormatter formatter, double delay = 0.8) : base(maze, Console.Out,
        formatter)
    {
        this.delay = delay;
    }

    protected override void Write(char sym)
    {
        writer.Write(sym);
        Thread.Sleep(TimeSpan.FromMilliseconds(delay));
    }
}