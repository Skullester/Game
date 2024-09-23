namespace Models.Fabric;

public abstract class MazeBuilder
{
    public string Name { get; }
    public IMaze Maze { get; } = new Maze();

    protected MazeBuilder(string name)
    {
        Name = name;
    }

    public void GenerateMaze()
    {
        Maze.Generate();
    }

    public abstract MazeBuilder SetSize();
}