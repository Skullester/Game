using Models.Maze;
using Models.Naming;

namespace Models.Player;

public abstract class A
{
}

public abstract class Player : A, INaming
{
    protected readonly IMaze maze;
    public ConsoleColor Color { get; }
    public abstract string Name { get; }
    public Point Location { get; private set; }
    public TimeSpan CoolDownTime { get; }

    protected Player(IMaze maze, ConsoleColor color, TimeSpan coolDownTime)
    {
        Color = color;
        CoolDownTime = coolDownTime;
        this.maze = maze;
    }

    protected abstract void SetDefaultValues();
    public abstract IEnumerable<Point> GetSkillPoints();

    public virtual void Move(Point point)
    {
        var x = Location.X - point.X;
        var y = point.Y + Location.Y;
        Location = new Point(x, y);
    }

    public virtual void Initialize()
    {
        Location = maze.StartPoint;
        SetDefaultValues();
    }
}