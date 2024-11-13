using Models.Maze;

namespace Models.Player;

[Show("Трейсер")]
public class Tracer : PlayerRole
{
    public Tracer(IMaze maze, double ratio, TimeSpan coolDown, int uses) : base(maze, ConsoleColor.Magenta,
        coolDown)
    {
        Skill = new TracerSkill(ratio, this, uses);
    }

    public override void MoveTo(Point newPoint)
    {
        Skill.Use();
        base.MoveTo(newPoint);
    }

    public override void MoveBy(Point offset)
    {
        Skill.Use();
        base.MoveBy(offset);
    }
    // public override IEnumerable<Point> GetSkillPoints() => tracesQueue;
}