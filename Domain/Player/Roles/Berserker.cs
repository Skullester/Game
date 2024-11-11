using Models.Maze;

namespace Models.Player;

public class Berserker : PlayerRole, IComplexRole
{
    public override string Name => "Берсерк";

    public IComplexSkill ComplexSkill => (Skill as IComplexSkill)!;

    public Berserker(IMaze maze, double ratio, TimeSpan coolDown) : base(maze,
        ConsoleColor.Magenta, coolDown)
    {
        Skill = new BerserkerSkill(ratio, maze, this);
    }
}