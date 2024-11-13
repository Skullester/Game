namespace Models.Player;

public interface ISkill
{
    public int RemainingUses { get; }
    IEnumerable<Point> View { get; }
    PlayerRole PlayerRole { get; }
    bool Use();
    void ResetValues();
}