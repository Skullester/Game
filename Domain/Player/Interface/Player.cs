using Models.Maze;
using Models.Naming;

namespace Models.Player;

public abstract class Player : INaming
{
    protected Skill skill = null!;
    protected readonly IMaze maze;

    public ConsoleColor Color { get; }

    public abstract string Name { get; }

    public Point Location { get; private set; }

    public virtual void Move(Point point)
    {
        Location += new Size(point);
    }

    public TimeSpan CoolDownTime { get; }

    protected Player(IMaze maze, ConsoleColor color, TimeSpan coolDownTime)
    {
        this.maze = maze;
        Color = color;
        CoolDownTime = coolDownTime;
    }

    protected virtual void SetDefaultValues()
    {
        skill.ResetValues();
    }


    // public abstract IEnumerable<Point> GetSkillPoints();


    public void UseSkill()
    {
        skill.Use();
    }

    public virtual void Initialize()
    {
        Location = maze.StartPoint;
        SetDefaultValues();
    }
}