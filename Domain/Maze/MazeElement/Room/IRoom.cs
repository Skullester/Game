namespace Models.Maze;

public interface IRoom : IMazeElement
{
    IRoom Clone();
    TimeSpan StayTime { get; }
}