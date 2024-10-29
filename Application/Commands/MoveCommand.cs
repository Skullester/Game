namespace Game;

public class MoveCommand
{
    private IMaze maze => manager.Maze;
    private readonly IGameManager manager;
    private Point Location => player.Location;
    private Player player => manager.Player;

    public MoveCommand(IGameManager manager)
    {
        this.manager = manager;
    }

    public void Execute(Point point)
    {
        if (!CheckBounds(point)) return;
        player.Move(point);
        var loc = Location;
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