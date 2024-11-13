namespace Models.Player;

public class TracerSkill : ISkill
{
    public PlayerRole PlayerRole { get; }

    public IEnumerable<Point> View
    {
        get
        {
            RemainingUses--;
            return tracesQueue;
        }
    }

    public int RemainingUses { get; private set; }

    public readonly int MaxUses;

    public readonly int MaxTraces;

    private const int multiplier = 50;
    private Queue<Point> tracesQueue = null!;
    private HashSet<Point> map = null!;
    private Point Location => PlayerRole.Location;

    public TracerSkill(double ratio, PlayerRole playerRole, int uses)
    {
        MaxTraces = (int)(ratio * multiplier);
        MaxUses = uses;
        PlayerRole = playerRole;
    }

    public void ResetValues()
    {
        tracesQueue = new Queue<Point>();
        map = new HashSet<Point>();
        RemainingUses = MaxUses;
    }

    public bool Use()
    {
        if (RemainingUses == 0 || !map.Add(Location)) return false;
        tracesQueue.Enqueue(Location);
        if (tracesQueue.Count > MaxTraces)
        {
            var dequeue = tracesQueue.Dequeue();
            map.Remove(dequeue);
        }

        return true;
    }
}