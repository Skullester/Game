namespace Models.Maze;

public interface IRoom : IMazeElement
{
    IRoom Clone();
    int StayTime { get; }
}