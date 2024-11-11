namespace Models.Player;

public interface ISkill
{
    IEnumerable<Point> View { get; }
    PlayerRole PlayerRole { get; }
    void Use();
    void ResetValues();
}