using Models.Maze;

namespace Models.Player;

[Show("Трейсер")]
public class Tracer : PlayerRole
{
    public Tracer(IMaze maze, double ratio, TimeSpan coolDown) : base(maze, ConsoleColor.Magenta,
        coolDown)
    {
        Skill = new TracerSkill(ratio, this);
    }

    public override void Move(Point point)
    {
        Skill.Use();
        base.Move(point);
    }

    // public override IEnumerable<Point> GetSkillPoints() => tracesQueue;
}