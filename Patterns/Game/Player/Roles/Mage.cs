using Patterns;

namespace ConsoleApp1.Player.Roles;

public class Mage : Player2
{
    public int CountHintsMoves { get; private set; }

    public override string Name => "Маг";

    public Mage(IMaze maze, int countHintsMoves) : base(maze)
    {
        CountHintsMoves = countHintsMoves;
    }


    public override void UseSkill()
    {
        throw new NotImplementedException();
    }
}