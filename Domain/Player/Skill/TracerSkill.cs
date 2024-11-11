namespace Models.Player;

public class TracerSkill : ISkill
{
    public PlayerRole PlayerRole { get; }
    public IEnumerable<Point> View => tracesQueue;
    public readonly int MaxValue;
    private const int multiplier = 50;
    private Queue<Point> tracesQueue = null!;

    private HashSet<Point> map = null!;


    // private LinkedList<Point> linkedList = null!;
    private Point Location => PlayerRole.Location;


    public TracerSkill(double ratio, PlayerRole playerRole)
    {
        MaxValue = (int)(ratio * multiplier);
        PlayerRole = playerRole;
    }

    public void ResetValues()
    {
        tracesQueue = new Queue<Point>();
        map = new HashSet<Point>();
    }

    public void Use()
    {
        if (!map.Add(Location))
            return;
        tracesQueue.Enqueue(Location);
        if (tracesQueue.Count > MaxValue)
        {
            var dequeue = tracesQueue.Dequeue();
            map.Remove(dequeue);
        }

        #region LinkedListWay

        /*linkedList.AddLast(Location);
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
        }*/

        /*static bool IsNextPointClose(LinkedListNode<Point> list)
        {
            var prevValue = list.Next!.Value;
            var lastValue = list.Value;
            return !(Math.Abs(lastValue.X - prevValue.X) > 1 || Math.Abs(prevValue.Y - lastValue.Y) > 1);
        }*/

        #endregion
    }
}