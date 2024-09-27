using System.Drawing;

namespace Models.Maze;

public interface IMaze : IEnumerable<IMazeElement>
{
    IMazeElement[,] Elements { get; }
    WallType WallType { get; }
    int Height { get; set; }
    int Width { get; set; }
    Point StartPoint { get; }
    Point ExitPoint { get; }
    void Generate();
    List<Point> GetPathList();
    IRoom Room { get; }
    IMazeElement this[int x, int y] { get; set; }
}