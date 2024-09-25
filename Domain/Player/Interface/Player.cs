using System.Drawing;
using Application.Naming;

namespace Models;

public abstract class Player : INaming
{
    protected readonly IMaze maze;
    public ConsoleColor Color { get; }
    public abstract string Name { get; }
    public Point Location { get; private set; }

    protected Player(IMaze maze, ConsoleColor color)
    {
        Color = color;
        this.maze = maze;
    }

    protected abstract void SetDefaultValues();
    public abstract IEnumerable<Point> GetSkillPoints();

    public virtual void Move(Point point)
    {
        var x = Math.Abs(Location.X - point.X);
        var y = point.Y + Location.Y;
        Location = new Point(x, y);
    }

    public virtual void Initialize()
    {
        Location = maze.StartPoint;
        SetDefaultValues();
    }
}