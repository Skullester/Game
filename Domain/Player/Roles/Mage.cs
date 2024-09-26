using System.Drawing;
using Models.Maze;

namespace Models.Player;

public class Mage : Player
{
    public const int HintsConst = 5;
    public const int HintLength = 5;
    public readonly int HintsCount;
    public int CurrentHintCount { get; private set; }

    public override string Name => "Маг";
    private Point[] exitPoints = null!;
    private Dictionary<Point, int> mapPointIndex = null!;

    public Mage(IMaze maze, int countHintsCount, TimeSpan coolDown) : base(maze, ConsoleColor.Blue, coolDown, false)
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
        Span<Point> span = exitPoints;
        if (mapPointIndex.TryGetValue(Location, out var index))
        {
            var slice = span.Slice(index).GetEnumerator();
        }

        //Span.Take(5), Dictionary<Point,int> where int - index, pointer;
        yield break;
    }

    public override void Move(Point point)
    {
        if (mapPointIndex.TryGetValue(Location, out var value))
        {
        }

        base.Move(point);
    }

    public override void Initialize()
    {
        var pointsList = new List<Point>();
        mapPointIndex = new Dictionary<Point, int>();
        /*do
        {mapPointIndex.add()
        } while (expression);*/
        exitPoints = pointsList.ToArray();
    }
}