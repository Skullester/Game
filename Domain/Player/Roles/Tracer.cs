using System.Diagnostics;
using Models.Maze;

namespace Models.Player;

public class Tracer : Player
{
    public override string Name => "Трейсер";

    public Tracer(IMaze maze, double ratio, TimeSpan coolDown) : base(maze, ConsoleColor.Magenta,
        coolDown)
    {
        skill = new TracerSkill(ratio, this);
    }

    public override void Move(Point point)
    {
        skill.Use();
        base.Move(point);
    }

    // public override IEnumerable<Point> GetSkillPoints() => tracesQueue;
}