using Models.Maze;

namespace Models.Player;

public abstract class PlayerRole
{
    public ISkill Skill { get; init; } = null!;
    public ConsoleColor Color { get; }
    public TimeSpan SkillCooldown { get; }

    public Point Location { get; private set; }

    protected readonly IMaze maze;


    protected PlayerRole(IMaze maze, ConsoleColor color, TimeSpan skillCooldown)
    {
        this.maze = maze;
        Color = color;
        SkillCooldown = skillCooldown;
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