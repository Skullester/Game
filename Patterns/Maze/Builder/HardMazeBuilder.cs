namespace Models.Fabric;

public class HardMazeBuilder : MazeBuilder
{
    public HardMazeBuilder(MazeFactory factory) : base("Сложный",factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 51;
        return this;
    }
}