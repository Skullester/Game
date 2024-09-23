using Models;

namespace UI;

public class Writer : MazeWriter //заглушка
{
    public Writer(IMaze maze, TextWriter writer, IMazeFormatter formatter) : base(writer, formatter, maze)
    {
    }

    public override string Name => "Заглушка";

    protected override void Write(char sym)
    {
        Console.WriteLine("print");
    }
}