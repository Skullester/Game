using System.Drawing;
using Models;
using Patterns.Naming;

namespace Game;

abstract class Command : INaming
{
    protected readonly IMaze maze;
    protected readonly HashSet<ConsoleKey> symbolsMap = new();
    protected Point Player => maze.PlayerPoint;
    public IReadOnlySet<ConsoleKey> SymbolsMap => symbolsMap;
    private byte step;

    protected Command(IMaze maze)
    {
        this.maze = maze;
        // ReSharper disable once VirtualMemberCallInConstructor
        InitializeSymbols();
    }

    protected abstract void InitializeSymbols();
    public abstract void Execute();

    protected void Execute(Point newPoint)
    {
        var i = Math.Abs(Player.X - newPoint.X);
        var j = newPoint.Y + Player.Y;
        if (CheckBounds(i, j))
        {
            (maze[i, j], maze[Player.X, Player.Y]) = (maze[Player.X, Player.Y], maze[i, j]);
            maze.PlayerPoint = new Point(i, j);
        }
    }

    public abstract string Name { get; }

    private bool CheckBounds(int x, int y)
    {
        var inBounds = true;
        if (maze[x, y] is IWall)
        {
            inBounds = false;
            if (maze.WallType.Effect == State.Death)
                GameManager.GetManager().State = GameState.Defeat;
        }

        return inBounds;
    }
}