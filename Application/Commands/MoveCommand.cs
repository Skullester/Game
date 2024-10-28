namespace Game;

public abstract class MoveCommand : Command
{
    protected MoveCommand(IMaze maze, char symbol, IGameManager manager, Player player) : base(maze, symbol, manager,
        player, true)
    {
    }

    public override void Execute() => Execute(GetDirection());

    protected abstract Point GetDirection();

    protected void Execute(Point point)
    {
        if (!CheckBounds(point)) return;
        player.Move(point);
        var loc = player.Location;
        if (maze[loc.X, loc.Y] is ExitRoom)
        {
            maze[loc.X, loc.Y] = new Room();
            manager.State = GameState.Victory;
        }
    }

    private bool CheckBounds(Point point)
    {
        var newPoint = Location + new Size(point);
        var inBounds = true;
        if (maze[newPoint.X, newPoint.Y] is IWall)
        {
            inBounds = false;
            if (maze.WallType.Effect == State.Death)
                manager.State = GameState.Defeat;
        }

        return inBounds;
    }
}