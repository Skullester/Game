namespace Models.Maze;

public interface IMaze : IEnumerable<IMazeElement>
{
    int Height { get; set; }
    int Width { get; set; }
    Point StartPoint { get; }
    Point ExitPoint { get; }
    IRoom Room { get; }
    IMazeElement this[int x, int y] { get; set; }
    void Generate();
}