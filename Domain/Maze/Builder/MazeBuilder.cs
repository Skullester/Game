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

    public abstract MazeBuilder SetSize();

    public void GenerateMaze()
    {
        Maze.Generate();
    }
}