namespace Models;

public interface IWallType
{
    State Effect { get; }
    public ConsoleColor Color { get; }
}