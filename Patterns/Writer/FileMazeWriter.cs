using Models;

namespace UI;

public class FileMazeWriter : MazeWriter
{
    public override string Name => "Файл";

    public FileMazeWriter(IMaze maze, TextWriter writer, MazeFormatter formatter) : base(maze, writer, formatter)
    {
    }

    protected override void Write(char sym)
    {
        writer.Write(sym);
        writer.Flush();
    }
}