using System.Drawing;
using Patterns.Naming;

namespace Models;

public abstract class Player : INaming
{
    protected IMaze maze;
    public ConsoleColor Color { get; }
    public abstract string Name { get; }
    public Point Location => maze.PlayerPoint;

    public Player(IMaze maze, ConsoleColor color)
    {
        Color = color;
        this.maze = maze;
    }

    public abstract void SetDefaultValues();
    public abstract IEnumerable<Point> UseSkill();
    public abstract void Initialize();
}