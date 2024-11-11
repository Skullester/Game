namespace Models.Player;

public class MageSkill : ISkill
{
    public IEnumerable<Point> View => Enumerable.Empty<Point>();
    public PlayerRole PlayerRole { get; }
    public readonly int MaxValue;

    public MageSkill(PlayerRole playerRole, double ratio, int constValue)
    {
        MaxValue = (int)(ratio * constValue);
        PlayerRole = playerRole;
    }

    public void Use()
    {
        throw new NotImplementedException();
    }

    public void ResetValues()
    {
        throw new NotImplementedException();
    }
}