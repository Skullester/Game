namespace Models;

public class Berserker : Player2
{
    public const int BreakableWallsCount = 4;
    public int BreakableWalls { get; private set; }
    public override string Name => "Берсерк";

    public Berserker(IMaze maze, int breakableWalls) : base(maze)
    {
        BreakableWalls = breakableWalls;
    }

    public override void UseSkill()
    {
        BreakableWalls--;
    }
}