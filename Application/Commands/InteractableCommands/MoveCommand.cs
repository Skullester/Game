namespace Game;

public class MoveCommand : Command, ICommandWithDirection, IUpdatableCommand
{
    public event Action? Updated;
    public Point Direction { get; set; }

    private readonly IGameManager gm;
    private IMaze maze => gm.Maze;

    private Point Location => PlayerRole.Location;

    private PlayerRole PlayerRole => gm.PlayerRole;

    public MoveCommand(IGameManager gm)
    {
        this.gm = gm;
    }

    public void Execute()
    {
        if (Direction == Point.Empty) return;
        var dir = Direction;
        if (!CheckBounds(dir)) return;
        PlayerRole.Move(dir);
        var loc = Location;
        if (maze[loc.X, loc.Y] is ExitRoom)
        {
            gm.SetVictory();
        }

        Updated?.Invoke();
    }

    private bool CheckBounds(Point point)
    {
        var newPoint = Location + new Size(point);
        var inBounds = true;
        if (maze[newPoint.X, newPoint.Y] is IWall)
        {
            inBounds = false;
            if (maze[newPoint.X, newPoint.Y] is InternalWall)
            {
                gm.CheckEffect(maze.WallType.Effect);
            }
        }

        return inBounds;
    }
}