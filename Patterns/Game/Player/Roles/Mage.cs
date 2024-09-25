using System.Drawing;

namespace Models;

public class Mage : Player
{
    public const int HintsMovesCount = 5;

    public int HintsMoves { get; private set; }

    public override string Name => "Маг";

    public Mage(IMaze maze, int countHintsMoves) : base(maze, ConsoleColor.Blue)
    {
        HintsMoves = countHintsMoves;
    }

    public override void SetDefaultValues()
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Point> UseSkill()
    {
        HintsMoves--;
        yield break;
    }

    public override void Initialize()
    {
        throw new NotImplementedException();
    }
}