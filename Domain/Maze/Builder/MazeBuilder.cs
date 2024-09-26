using Models.Maze;

namespace Models.Fabric;

public abstract class MazeBuilder
{
    public string Name { get; }
    public IMaze Maze { get; }

    protected MazeBuilder(string name, MazeFactory factory)
    {
        Name = name;
        Maze = new RectangularMaze(factory);
    }

    public void GenerateMaze()
    {
        Maze.Generate();
    }

    public abstract MazeBuilder SetSize();
}