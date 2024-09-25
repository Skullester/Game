using System.Drawing;

namespace Models;

public class Mage : Player
{
    public const int HintsConst = 5;
    public const int HintLength = 5;
    public readonly int HintsCount;
    public int CurrentHintCount { get; private set; }

    public override string Name => "Маг";
    private List<Point>? exitPoints;

    public Mage(IMaze maze, int countHintsCount) : base(maze, ConsoleColor.Blue)
    {
        HintsCount = countHintsCount;
    }

    protected override void SetDefaultValues()
    {
        CurrentHintCount = HintsCount;
    }

    public override IEnumerable<Point> GetSkillPoints()
    {
        CurrentHintCount--;
        yield break;
    }

    public override void Initialize()
    {
        exitPoints = new List<Point>();
    }
}