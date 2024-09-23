namespace Models.Fabric;

public class MadnessMazeBuilder : MazeBuilder
{
    public MadnessMazeBuilder() : base("Безумный")
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 60;
        return this;
    }
}