namespace Models.Maze;
public class ExternalWall : IWall
{
    public bool IsVisited { get; set; }

    public int Distance { get; set; }

    public WallType Type { get; }

    public ExternalWall(WallType type)
    {
        Type = type;
    }

    public IWall Clone() => (MemberwiseClone() as ExternalWall)!;
}