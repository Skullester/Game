using Models.Maze;

namespace Models.Player;

public class BerserkerSkill : IComplexSkill
{
    public readonly int MaxHits;
    public int RemainingUses { get; private set; }
    public PlayerRole PlayerRole { get; }
    public Point Direction { get; set; }
    public IEnumerable<Point> View => points;
    private const int maxHitLength = 3;
    private const double multiplier = 10;
    private List<Point> points = null!;
    private Random random = null!;
    private readonly IMaze maze;
    private Point Location => PlayerRole.Location;

    public BerserkerSkill(double ratio, IMaze maze, PlayerRole playerRole)
    {
        MaxHits = (int)(ratio * multiplier);
        this.maze = maze;
        PlayerRole = playerRole;
    }

    public void ResetValues()
    {
        random = new Random();
        points = new List<Point>();
        RemainingUses = MaxHits;
    }

    public bool Use()
    {
        if (RemainingUses == 0 || Direction == Point.Empty) return false;
        points.Clear();
        RemainingUses--;
        var hitPoint = Location + new Size(Direction);
        var hitLength = random.Next(1, maxHitLength + 1);
        for (var i = 0; i < hitLength; i++)
        {
            if (!InBounds()) break;
            var x = hitPoint.X;
            var y = hitPoint.Y;
            if (maze[x, y] is InternalWall)
            {
                points.Add(new Point(x, y));
                maze[x, y] = maze.Room.Clone();
            }

            hitPoint += new Size(Direction);
        }

        return true;

        bool InBounds() => hitPoint.X > 0 && hitPoint.X < maze.Height && hitPoint.Y > 0 && hitPoint.Y < maze.Width;
    }
}