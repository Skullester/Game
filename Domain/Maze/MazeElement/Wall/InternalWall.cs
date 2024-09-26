namespace Models.Maze;
public class InternalWall : IWall
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }

    public WallType Type { get; }

    public InternalWall(WallType type)
    {
        Type = type;
    }

    public IWall Clone() => (MemberwiseClone() as IWall)!;
}