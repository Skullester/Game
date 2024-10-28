using Models.Maze;

namespace Models.Player;

public class Berserker : Player
{
    public override string Name => "Берсерк";

    public Berserker(IMaze maze, double ratio, TimeSpan coolDown) : base(maze,
        ConsoleColor.Magenta, coolDown)
    {
        skill = new BerserkerSkill(ratio, maze, this);
    }

    /*public override IEnumerable<Point> GetSkillPoints()
    {
        return null;
    }*/
}