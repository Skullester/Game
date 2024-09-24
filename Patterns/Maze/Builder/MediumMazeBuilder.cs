namespace Models.Fabric;

public class MediumMazeBuilder : MazeBuilder
{
    public MediumMazeBuilder(MazeFactory factory) : base("Средний",factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 31;
        return this;
    }
}