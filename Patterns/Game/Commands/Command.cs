using System.Drawing;
using Models;
using Patterns.Naming;

namespace Game;

public abstract class Command : INaming
{
    private readonly IMaze maze;
    protected readonly HashSet<ConsoleKey> symbolsMap = new();
    protected Point Player => maze.PlayerPoint;
    public IReadOnlySet<ConsoleKey> SymbolsMap => symbolsMap;
    protected readonly GameManager manager;
    public char Symbol { get; }

    protected Command(IMaze maze, char symbol)
    {
        this.maze = maze;
        manager = GameManager.GetManager();
        Symbol = symbol;
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
            if (maze[i, j] is ExitRoom)
            {
                maze[i, j] = new Room();
                manager.State = GameState.Victory;
            }

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