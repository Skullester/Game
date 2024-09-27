using System.Drawing;
using Models.Maze;

namespace Models.Player;

public class Mage : Player
{
    public const int HintsConst = 5;
    public const int HintLength = 10;
    public readonly int HintsCount;
    public int CurrentHintCount { get; private set; }

    public override string Name => "Маг";
    private Point[] exitPoints = null!;
    private Dictionary<Point, int> mapPointIndex = null!;

    public Mage(IMaze maze, int countHintsCount, TimeSpan coolDown) : base(maze, ConsoleColor.DarkBlue, coolDown)
    {
        HintsCount = countHintsCount;
    }

    protected override void SetDefaultValues()
    {
        CurrentHintCount = HintsCount;
        mapPointIndex = new Dictionary<Point, int>();
    }

    public override IEnumerable<Point> GetSkillPoints()
    {
        if (CurrentHintCount == 0) return [];
        CurrentHintCount--;
        Span<Point> span = exitPoints;
        if (mapPointIndex.TryGetValue(Location, out var index))
        {
        }

        // var slice = span.Slice(index, HintLength);

        return exitPoints;
        //Span.Take(5), Dictionary<Point,int> where int - index, pointer;
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
        base.Initialize();
        exitPoints = maze.GetPathList()
            .ToArray();
        for (var i = 0; i < exitPoints.Length; i++)
        {
            mapPointIndex.Add(exitPoints[i], i);
        }
    }
}