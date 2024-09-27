using System.Drawing;
using Models.Maze;

namespace Models.Player;

public class Tracer : Player
{
    public const int TracesConst = 50;

    public readonly int MaxTraces;

    // private Queue<Point> tracesQueue = null!;
    private LinkedList<Point> linkedList = null!;
    private HashSet<Point> map = null!;

    public override string Name => "Трейсер";

    public Tracer(IMaze maze, int maxTraces, TimeSpan coolDown) : base(maze, ConsoleColor.Magenta, coolDown)
    {
        MaxTraces = maxTraces;
    }

    public override void Move(Point point)
    {
        AddMoveToList();
        base.Move(point);
    }

    private void AddMoveToList()
    {
        if (!map.Add(Location)) return;
        linkedList.AddLast(Location);
        if (linkedList.Count > MaxTraces)
        {
            map.Remove(linkedList.First!.Value);
            linkedList.RemoveFirst();
            var first = linkedList.First!;
            while (first != linkedList.Last)
            {
                if (!IsNextPointClose(first))
                {
                    map.Remove(linkedList.First.Value);
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
        linkedList = new LinkedList<Point>();
        map = new HashSet<Point>();
    }

    public override IEnumerable<Point> GetSkillPoints() => linkedList;
}