using System.Drawing;

namespace Models;

public class Berserker : Player
{
    public const int BreakableWallsConst = 10;
    public readonly int BreakableWallsCount;
    public int CurrentBreakableWalls { get; private set; }
    public override string Name => "Берсерк";
    private Random? random;

    public Berserker(IMaze maze, int breakableWalls) : base(maze, ConsoleColor.Red)
    {
        BreakableWallsCount = breakableWalls;
        CurrentBreakableWalls = breakableWalls;
    }

    public override void SetDefaultValues()
    {
        CurrentBreakableWalls = BreakableWallsCount;
    }

    public override void Initialize()
    {
        random = new Random();
    }

    public override IEnumerable<Point> UseSkill()
    {
        var startPoint = Location - new Size(1, 1);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CurrentBreakableWalls == 0) yield break;
                var x = startPoint.X + i;
                var y = startPoint.Y + j;
                var isSuccess = random.Next(0, 2) == 0;
                if (isSuccess && maze[x, y] is InternalWall)
                {
                    maze[x, y] = maze.Room.Clone();
                    CurrentBreakableWalls--;
                    yield return new Point(x, y);
                }
            }
        }
    }
}