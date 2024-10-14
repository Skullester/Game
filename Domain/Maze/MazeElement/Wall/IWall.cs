namespace Models.Maze;

public interface IWall : IMazeElement
{
    WallType Type { get; }
    IWall Clone();
}