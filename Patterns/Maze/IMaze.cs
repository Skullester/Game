using System.Drawing;

namespace Models;

public interface IMaze : IEnumerable<IMazeElement>
{
    IMazeElement[,] Elements { get; }
    int Height { get; set; }
    int Width { get; set; }
    Point StartPoint { get; }
    void Generate();
}