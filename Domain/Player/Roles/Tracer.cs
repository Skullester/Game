using System.Drawing;
using Models.Maze;

namespace Models.Player;

public class Tracer : Player
{
    public const int TracesConst = 30;

    public readonly int MaxTraces;

    // private Queue<Point> tracesQueue = null!;
    private LinkedList<Point> linkedList = null!;

    public override string Name => "Трейсер";

    public Tracer(IMaze maze, int maxTraces, TimeSpan coolDown) : base(maze, ConsoleColor.DarkMagenta, coolDown, false)
    {
        MaxTraces = maxTraces;
    }

    public override void Initialize()
    {
        // tracesQueue = new Queue<Point>();
        linkedList = new LinkedList<Point>();
        base.Initialize();
    }

    public override void Move(Point point)
    {
        AddMoveToList();
        base.Move(point);
    }

    private void AddMoveToList()
    {
        if (linkedList.Contains(Location)) return;
        linkedList.AddLast(Location);
        if (linkedList.Count > MaxTraces)
        {
            linkedList.RemoveFirst();
            LinkedListNode<Point> first = linkedList.First!;
            while (first != linkedList.Last)
            {
                if (!IsNextPointClose(first))
                {
                    linkedList.RemoveFirst();
                    first = linkedList.First;
                }
                else
                    first = first.Next!;
            }
        }

        static bool IsNextPointClose(LinkedListNode<Point> list)
        {
            var prevValue = list.Next!.Value;
            var lastValue = list.Value;
            return !(Math.Abs(lastValue.X - prevValue.X) > 1 || Math.Abs(prevValue.Y - lastValue.Y) > 1);
        }

        /*
        if (tracesQueue.Contains(Location)) return;
        tracesQueue.Enqueue(Location);
        if (tracesQueue.Count > MaxTraces)
        {
            tracesQueue.Dequeue();
        }*/
    }

    protected override void SetDefaultValues()
    {
        linkedList.Clear();
    }

    public override IEnumerable<Point> GetSkillPoints() => linkedList;
}