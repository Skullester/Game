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

    /// <summary>
    /// Moving player
    /// </summary>
    /// <param name="newPoint">Moving player to newPoint</param>
    public virtual void MoveTo(Point newPoint)
    {
        Location = newPoint;
    }

    /// <summary>
    /// Moving player
    /// </summary>
    /// <param name="offset">Moving player by offset </param>
    public virtual void MoveBy(Point offset)
    {
        Location += new Size(offset);
    }

    public virtual void Initialize()
    {
        Location = maze.StartPoint;
        SetDefaultValues();
    }

    protected virtual void SetDefaultValues() => Skill.ResetValues();

    public bool UseSkill() => Skill.Use();
}