namespace Models.Fabric;

public class MadnessMazeBuilder : MazeBuilder
{
    public MadnessMazeBuilder(MazeFactory factory) : base("Безумный", factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = 29;
        Maze.Width = 101;
        return this;
    }
}