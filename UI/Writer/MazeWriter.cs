using Infrastructure;
using Models.Maze;
using Models.Naming;

namespace UI.Displaying;

public abstract class MazeWriter : INaming
{
    public abstract string Name { get; }
    protected readonly TextWriter writer;
    private readonly IEnumerable<char> mazeChars;
    private readonly IMaze maze;
    public int Delay { get; }

    protected MazeWriter(IEnumerable<char> mazeChars, TextWriter writer, IMaze maze, int delay)
    {
        this.mazeChars = mazeChars;
        this.maze = maze;
        this.writer = writer;
        Delay = delay;
    }

    protected MazeWriter(IMaze maze, TextWriter writer, MazeFormatter mazeFormatter, int delay) : this(
        maze.ParseToChar(mazeFormatter), writer, maze, delay)
    {
    }

    public void Write()
    {
        var counter = 0;
        foreach (var item in mazeChars)
        {
            Write(item);
            Thread.Sleep(Delay);
            if (++counter % maze!.Width == 0)
                Write('\n');
        }

        writer.Close();
    }

    protected abstract void Write(char sym);
}