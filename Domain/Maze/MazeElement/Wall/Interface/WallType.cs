namespace Models.Maze;

public abstract class WallType
{
    public State Effect { get; }

    public ConsoleColor Color { get; }

    protected WallType(State effect, ConsoleColor color)
    {
        Effect = effect;
        Color = color;
    }
}