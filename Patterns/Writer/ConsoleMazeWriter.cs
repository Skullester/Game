using Models;

namespace UI;

public class ConsoleMazeWriter : MazeWriter
{
    public double Delay { get; set; }

    public override string Name => "Консоль";

    public ConsoleMazeWriter(IMaze? maze, MazeFormatter formatter, double delay = 0) : base(maze, Console.Out,
        formatter)
    {
        Delay = delay;
    }

    protected override void Write(char sym)
    {
        writer.Write(sym);
        Thread.Sleep( /*TimeSpan.FromMilliseconds(Delay)*/0);
    }
}