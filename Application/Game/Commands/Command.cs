using System.Drawing;
using Models.Maze;
using Models.Naming;
using Models.Player;

namespace Game;

public abstract class Command : INaming
{
    public IReadOnlySet<ConsoleKey> SymbolsMap => symbolsMap;
    public char Symbol { get; }
    protected readonly IMaze maze;
    protected readonly HashSet<ConsoleKey> symbolsMap = new();
    public event Action<IEnumerable<Point>>? Perfomed;
    protected Point Location => player.Location;
    protected readonly IGameManager manager;
    protected readonly Player player;

    protected Command(IMaze maze, char symbol, IGameManager manager, Player player)
    {
        this.maze = maze;
        Symbol = symbol;
        this.manager = manager;
        this.player = player;
        // ReSharper disable once VirtualMemberCallInConstructor
        InitializeSymbols();
    }

    protected abstract void InitializeSymbols();
    public abstract bool Execute();
    public abstract string Name { get; }

    protected virtual void OnPerfomed(IEnumerable<Point> obj)
    {
        Perfomed?.Invoke(obj);
    }
}