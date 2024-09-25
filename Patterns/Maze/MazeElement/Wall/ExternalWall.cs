namespace Models;

public class ExternalWall : IWall
{
    public bool IsVisited { get; set; }

    public int Distance { get; set; }

    public IWallType Type { get; }

    public ExternalWall(IWallType type)
    {
        Type = type;
    }

    public IWall Clone() => (MemberwiseClone() as ExternalWall)!;
}