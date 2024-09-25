using System.Drawing;
using Game;

namespace Models;

public class Tracer : Player
{
    public const int TracesConst = 15;
    public readonly int MaxTraces;
    private Queue<Point> tracesQueue = new();
    public override string Name => "Трейсер";

    public override void Initialize()
    {
        GameManager.GetManager().Perfomed += () =>
        {
            if (tracesQueue.Contains(Location)) return;
            tracesQueue.Enqueue(Location);
            if (tracesQueue.Count > MaxTraces)
            {
                tracesQueue.Dequeue();
            }
        };
    }

    public Tracer(IMaze maze, int maxTraces) : base(maze, ConsoleColor.DarkMagenta)
    {
        MaxTraces = maxTraces;
    }

    public override void SetDefaultValues()
    {
        tracesQueue.Clear();
    }

    public override IEnumerable<Point> UseSkill() => tracesQueue;
}