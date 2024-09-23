namespace Models.Fabric;

public class EasyMazeBuilder : MazeBuilder
{
    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 20;
        return this;
    }

    public EasyMazeBuilder() : base("Легкий")
    {
    }
}