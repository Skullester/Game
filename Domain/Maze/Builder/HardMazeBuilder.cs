namespace Models.Fabric;

public class HardMazeBuilder : MazeBuilder
{
    public HardMazeBuilder(MazeFactory factory) : base("Сложный", factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = 27;
        Maze.Width = 80;
        return this;
    }
}