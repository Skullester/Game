﻿using Models.Maze;

namespace Models.Player;

public class Berserker : Player
{
    private const int breakableWallsConst = 10;
    public readonly int BreakableWallsCount;
    public int CurrentBreakableWalls { get; private set; }
    public override string Name => "Берсерк";
    private Random random = null!;

    public Berserker(IMaze maze, double ratio, TimeSpan coolDown) : base(maze, ConsoleColor.Magenta, coolDown)
    {
        BreakableWallsCount = (int)(ratio * breakableWallsConst);
        CurrentBreakableWalls = breakableWallsConst;
    }

    protected override void SetDefaultValues()
    {
        CurrentBreakableWalls = BreakableWallsCount;
        random = new Random();
    }

    public override IEnumerable<Point> GetSkillPoints()
    {
        var startPoint = Location - new Size(1, 1);
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
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