using Models.Maze;

namespace Models.Player;

[Show("Берсерк")]
public class Berserker : PlayerRole, IComplexRole
{

    public IComplexSkill ComplexSkill => (Skill as IComplexSkill)!;

    public Berserker(IMaze maze, double ratio, TimeSpan coolDown) : base(maze,
        ConsoleColor.Magenta, coolDown)
    {
        Skill = new BerserkerSkill(ratio, maze, this);
    }
}