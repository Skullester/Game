namespace Models;

public class InternalWall : IWall
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }

    public IWallType Type { get; }

    public InternalWall(IWallType type)
    {
        Type = type;
    }

    public IWall Clone() => (MemberwiseClone() as IWall)!;
}