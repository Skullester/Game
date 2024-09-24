using Models;
using Infrastructure;
using Patterns.Naming;

namespace UI;

public abstract class MazeWriter : INaming
{
    public abstract string Name { get; }
    protected readonly TextWriter writer;
    private readonly IEnumerable<char> mazeChars;
    private readonly IMaze maze;

    protected MazeWriter(IEnumerable<char> mazeChars, TextWriter writer, IMaze maze)
    {
        this.mazeChars = mazeChars;
        this.maze = maze;
        this.writer = writer;
    }

    protected MazeWriter(IMaze maze, TextWriter writer, MazeFormatter mazeFormatter) : this(
        maze.ParseToChar(mazeFormatter), writer, maze)
    {
    }

    public void Write()
    {
        var counter = 0;
        foreach (var item in mazeChars)
        {
            Write(item);
            if (++counter % maze.Width == 0)
                Write('\n');
        }

        writer.Close();
    }

    protected abstract void Write(char sym);
}