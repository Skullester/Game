namespace Models.Fabric;

public class MediumMazeBuilder : MazeBuilder
{
    public MediumMazeBuilder(MazeFactory factory) : base("Средний", factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = 25;
        Maze.Width = 61;
        return this;
    }
}