using Models.Maze;
using Models.Naming;

namespace Models.Player;

public abstract class PlayerRole : INaming
{
    public ISkill Skill { get; init; } = null!;
    public ConsoleColor Color { get; }
    public TimeSpan CoolDownTime { get; }

    public abstract string Name { get; }

    public Point Location { get; private set; }

    protected readonly IMaze maze;


    protected PlayerRole(IMaze maze, ConsoleColor color, TimeSpan coolDownTime)
    {
        this.maze = maze;
        Color = color;
        CoolDownTime = coolDownTime;
    }

    public virtual void Move(Point point)
    {
        Location += new Size(point);
    }

    public virtual void Initialize()
    {
        Location = maze.StartPoint;
        SetDefaultValues();
    }

    protected virtual void SetDefaultValues() => Skill.ResetValues();

    public void UseSkill() => Skill.Use();
}