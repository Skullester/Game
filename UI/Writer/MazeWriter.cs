using Infrastructure;
using Models;

namespace UI.Displaying;

public abstract class MazeWriter : INaming
{
    public abstract string Name { get; }
    protected readonly TextWriter writer;
    private readonly IEnumerable<(char sym, IMazeElement el)> mazeCharsColors;
    private readonly IMaze maze;
    public int Delay { get; }

    protected MazeWriter(IEnumerable<(char sym, IMazeElement el)> mazeCharsColors, TextWriter writer, IMaze maze,
        int delay)
    {
        this.mazeCharsColors = mazeCharsColors;
        this.maze = maze;
        this.writer = writer;
        Delay = delay;
    }

    protected MazeWriter(IMaze maze, TextWriter writer, MazeFormatter mazeFormatter, int delay) : this(
        maze.ParseToTuple(mazeFormatter), writer, maze, delay)
    {
    }

    public void Write()
    {
        using var disposingWriter = this.writer;
        var counter = 0;
        foreach (var (sym, el) in mazeCharsColors)
        {
            Write(sym, el);
            Thread.Sleep(Delay);
            if (++counter % maze.Width == 0)
                Write('\n', el);
        }
    }

    protected virtual void Write(char sym, IMazeElement el)
    {
        writer.Write(sym);
    }
}