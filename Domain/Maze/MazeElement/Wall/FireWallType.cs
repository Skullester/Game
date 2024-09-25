namespace Models;

public class FireWallType : IWallType
{
    public ConsoleColor Color { get; } = ConsoleColor.Red;

    public State Effect => State.Death;
}