namespace Models.Maze;

public interface IWall : IMazeElement, IColorable
{
    WallType Type { get; }
    IWall Clone();
}