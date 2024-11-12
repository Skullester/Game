namespace Models.Maze;

public abstract class WallType
{
    public Effect Effect { get; }

    public ConsoleColor Color { get; }

    protected WallType(Effect effect, ConsoleColor color)
    {
        Effect = effect;
        Color = color;
    }
}