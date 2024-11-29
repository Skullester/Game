namespace Game;

public class MoveCommand : ICommandWithDirection, IMapUpdatableCommand
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
        var newPoint = Location + new Size(Direction);
        if (!CheckBoundsIn(newPoint)) return;
        PlayerRole.MoveTo(newPoint);
        var loc = Location;
        if (maze[loc.X, loc.Y] is ExitRoom)
        {
            gm.SetVictory();
        }

        Updated?.Invoke();
    }

    private bool CheckBoundsIn(Point point)
    {
        var inBounds = true;
        if (maze[point.X, point.Y] is IWall wall)
        {
            inBounds = false;
            gm.CheckWallEffect(wall.Type.Effect);
        }

        return inBounds;
    }
}