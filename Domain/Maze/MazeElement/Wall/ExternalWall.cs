namespace Models.Maze;

public class ExternalWall : IWall
{
    public WallType Type { get; }
    public ConsoleColor Color => Type.Color;

    public ExternalWall(WallType type)
    {
        Type = type;
    }

    public IWall Clone() => (MemberwiseClone() as ExternalWall)!;
}