namespace Models;

public interface IWall : IMazeElement
{
    IWallType Type { get; }
    IWall Clone();
}