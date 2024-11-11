using Models.Maze;

namespace Models.Player;

public class BerserkerSkill : IComplexSkill
{
    public Point Direction { get; set; }
    public IEnumerable<Point> View => points;
    public PlayerRole PlayerRole { get; }
    public readonly int MaxValue;
    private const int MaxHitLength = 3;
    private const double multiplier = 10;
    private List<Point> points = null!;
    private Random random = null!;
    private readonly IMaze maze;
    private int value;
    private Point Location => PlayerRole.Location;

    public BerserkerSkill(double ratio, IMaze maze, PlayerRole playerRole)
    {
        MaxValue = (int)(ratio * multiplier);
        this.maze = maze;
        PlayerRole = playerRole;
    }

    public void ResetValues()
    {
        random = new Random();
        points = new List<Point>();
        value = MaxValue;
    }

    public void Use()
    {
        if (value == 0 || Direction == Point.Empty) return;
        points.Clear();
        var hitStart = Location + new Size(Direction);
        var hitLength = random.Next(1, MaxHitLength + 1);
        for (var i = 0; i < hitLength; i++)
        {
            var x = hitStart.X;
            var y = hitStart.Y;
            if (maze[x, y] is InternalWall)
            {
                points.Add(new Point(x, y));
                maze[x, y] = maze.Room.Clone();
            }

            hitStart += new Size(Direction);
        }

        value--;
    }
}