namespace Models.Player;

public abstract class Skill
{
    public readonly int MaxValue;

    protected Player Player { get; }

    protected Point Location => Player.Location;

    protected Skill(Player player, double ratio, int constValue)
    {
        Player = player;
        MaxValue = (int)(ratio * constValue);
    }

    public abstract void Use();

    public abstract void ResetValues();
}