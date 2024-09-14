using Patterns;

namespace ConsoleApp1.Player.Roles;

public class Berserker : Player2
{
    public int BreakableWallsCount { get; private set; }
    public override string Name => "Берсерк";

    public Berserker(IMaze maze, int breakableWallsCount) : base(maze)
    {
        BreakableWallsCount = breakableWallsCount;
    }

    public override void UseSkill()
    {
        BreakableWallsCount--;
    }
}