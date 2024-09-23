using Models;

namespace UI;

public class FileMazeWriter : MazeWriter
{
    public FileMazeWriter(IMaze maze, TextWriter writer, IMazeFormatter formatter) : base(writer, formatter, maze)
    {
    }

    public override string Name => "Файл";

    protected override void Write(char sym)
    {
        writer.Write(sym);
        writer.Flush();
    }
}