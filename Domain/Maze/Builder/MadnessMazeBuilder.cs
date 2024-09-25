namespace Models.Fabric;

public class MadnessMazeBuilder : MazeBuilder
{
    public MadnessMazeBuilder(MazeFactory factory) : base("Безумный",factory)
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 61;
        return this;
    }
}