namespace Models;

public interface IRoom : IMazeElement
{
    IRoom Clone();
    int Time { get; }
}