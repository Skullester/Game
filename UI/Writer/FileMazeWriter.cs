using Models.Maze;

namespace UI.Displaying;

public class FileMazeWriter : MazeWriter
{
    public override string Name => "Файл";

    public FileMazeWriter(IMaze maze, TextWriter writer, MazeFormatter formatter, int delay) : base(maze, writer,
        formatter, delay)
    {
    }

    protected override void Write(char sym)
    {
        writer.Write(sym);
        writer.Flush();
    }
}