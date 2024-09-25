using System.Drawing;

namespace Models;

public interface IMaze : IEnumerable<IMazeElement>
{
    IMazeElement[,] Elements { get; }
    IWallType WallType { get; }
    int Height { get; set; }
    int Width { get; set; }
    Point StartPoint { get; }
    void Generate();
    IRoom Room { get; }
    IMazeElement this[int x, int y] { get; set; }
}