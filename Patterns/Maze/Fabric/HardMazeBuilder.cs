namespace Models.Fabric;

public class HardMazeBuilder : MazeBuilder
{
    public HardMazeBuilder() : base("Сложный")
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 50;
        return this;
    }
}