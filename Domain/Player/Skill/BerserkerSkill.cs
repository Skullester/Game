using Models.Maze;

namespace Models.Player;

public class BerserkerSkill : Skill
{
    private Random random = null!;
    private readonly IMaze maze;
    private int value;

    public BerserkerSkill(double ratio, IMaze maze, Player player) : base(player, ratio, 10)
    {
        this.maze = maze;
    }

    public override void ResetValues()
    {
        random = new Random();
        value = MaxValue;
    }

    public override void Use()
    {
        var startPoint = Location - new Size(1, 1);
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (value == 0) return;
                var x = startPoint.X + i;
                var y = startPoint.Y + j;
                var isSuccess = random.Next(0, 2) == 0;
                if (isSuccess && maze[x, y] is InternalWall)
                {
                    maze[x, y] = maze.Room.Clone();
                    value--;
                    // yield return new Point(x, y);
                }
            }
        }
    }
}