namespace Models.Fabric;

public class EasyMazeBuilder : MazeBuilder
{
    public EasyMazeBuilder(MazeFactory factory) : base("Легкий", factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = 21;
        Maze.Width = 17;
        return this;
    }
}