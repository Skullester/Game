namespace Models.Fabric;

public class EasyMazeBuilder : MazeBuilder
{
    public EasyMazeBuilder(MazeFactory factory) : base("Легкий",factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 21;
        return this;
    }
}