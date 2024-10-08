﻿// ReSharper disable VirtualMemberCallInConstructor

namespace Game;

public abstract class Command : INaming
{
    public event Action<IEnumerable<Point>>? Perfomed;
    public IReadOnlySet<ConsoleKey> KeyMap => keyMap;
    public char Symbol { get; }
    public abstract string Name { get; }
    protected readonly IMaze maze;
    protected readonly HashSet<ConsoleKey> keyMap = new();
    protected Point Location => player.Location;
    protected readonly IGameManager manager;
    protected readonly Player player;


    protected Command(IMaze maze, char symbol, IGameManager manager, Player player)
    {
        this.maze = maze;
        Symbol = symbol;
        this.manager = manager;
        this.player = player;
        InitializeSymbols();
    }

    protected abstract void InitializeSymbols();
    public abstract bool Execute();

    protected virtual void OnPerfomed(IEnumerable<Point> obj)
    {
        Perfomed?.Invoke(obj);
    }
}