namespace Models.Maze;

public class InternalWall : IWall
{
    public WallType Type { get; }

    public InternalWall(WallType type)
    {
        Type = type;
    }

    public IWall Clone() => (MemberwiseClone() as IWall)!;
}