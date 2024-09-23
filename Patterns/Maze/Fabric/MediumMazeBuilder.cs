namespace Models.Fabric;

public class MediumMazeBuilder : MazeBuilder
{
    public MediumMazeBuilder() : base("Средний")
    {
    }

    public override MazeBuilder SetSize()
    {
        Maze.Height = Maze.Width = 30;
        return this;
    }
}