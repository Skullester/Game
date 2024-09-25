using System.Drawing;
using Models;

namespace Game;

public abstract class MoveCommand : Command
{
    protected MoveCommand(IMaze maze, char symbol, IGameManager manager, Player player) : base(maze, symbol, manager,
        player)
    {
    }

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
        // (maze[i, j], maze[Location.X, Location.Y]) = (maze[Location.X, Location.Y], maze[i, j]);
    }

    private bool CheckBounds(Point point)
    {
        var i = Math.Abs(Location.X - point.X);
        var j = point.Y + Location.Y;
        var inBounds = true;
        if (maze[i, j] is IWall)
        {
            inBounds = false;
            if (maze.WallType.Effect == State.Death)
                manager.State = GameState.Defeat;
        }

        return inBounds;
    }
}