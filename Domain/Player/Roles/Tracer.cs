using System.Drawing;

namespace Models;

public class Tracer : Player
{
    public const int TracesConst = 15;
    public readonly int MaxTraces;
    private Queue<Point> tracesQueue = new();
    public override string Name => "Трейсер";

    public override void Initialize()
    {
        tracesQueue = new Queue<Point>();
        base.Initialize();
    }

    public override void Move(Point point)
    {
        AddMoveToQueue();
        base.Move(point);
    }

    private void AddMoveToQueue()
    {
        if (tracesQueue.Contains(Location)) return;
        tracesQueue.Enqueue(Location);
        if (tracesQueue.Count > MaxTraces)
        {
            tracesQueue.Dequeue();
        }
    }

    public Tracer(IMaze maze, int maxTraces) : base(maze, ConsoleColor.DarkMagenta)
    {
        MaxTraces = maxTraces;
    }

    protected override void SetDefaultValues()
    {
        tracesQueue.Clear();
    }

    public override IEnumerable<Point> GetSkillPoints() => tracesQueue;
}