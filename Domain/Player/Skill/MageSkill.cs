namespace Models.Player;

public class MageSkill : Skill
{
    public MageSkill(Player player, double ratio, int constValue) : base(player, ratio, constValue)
    {
    }

    public override void Use()
    {
        throw new NotImplementedException();
    }

    public override void ResetValues()
    {
        throw new NotImplementedException();
    }
}