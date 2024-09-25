namespace Models;

public class DefaultWallType : IWallType
{
    public ConsoleColor Color { get; } = ConsoleColor.Cyan;
    public State Effect => State.None;
}