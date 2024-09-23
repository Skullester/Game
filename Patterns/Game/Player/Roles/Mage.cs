namespace Models;
public class Mage : Player2
{
    public const int HintsMovesCount = 5;

    public int HintsMoves { get; private set; }

    public override string Name => "Маг";

    public Mage(IMaze maze, int countHintsMoves) : base(maze)
    {
        HintsMoves = countHintsMoves;
    }

    public override void UseSkill()
    {
        HintsMoves--;
    }
}