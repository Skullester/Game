﻿using System.Diagnostics;
using Models.Fabric;
using Models.Maze;
using Models.Player;

namespace Game;

public sealed class GameManager : IGameManager
{
    private static GameManager? instance;
    public GameState State { get; set; }
    public Player Player { get; }
    public IMaze Maze { get; private set; } = null!;
    public MazeBuilder Builder { get; }
    private Stopwatch stopwatch = null!;

    private GameManager(Player player, MazeBuilder builder)
    {
        Player = player;
        Builder = builder;
    }

    private void CreateMaze()
    {
        Builder.SetSize();
        Builder.GenerateMaze();
        Maze = Builder.Maze;
    }

    public static GameManager GetManager(Player player, MazeBuilder mazeBuilder)
    {
        instance ??= new GameManager(player, mazeBuilder);
        return instance;
    }

    public bool Execute(Command command)
    {
        if (!CheckTimePenalty()) return false;
        var isExecuted = command.Execute();
        return isExecuted;
    }

    public void Initialize()
    {
        CreateMaze();
        Player.Initialize();
        State = GameState.Play;
        stopwatch = new Stopwatch();
    }

    private bool CheckTimePenalty()
    {
        if (!stopwatch.IsRunning)
        {
            stopwatch.Start();
            return true;
        }

        if (stopwatch.Elapsed > Maze.Room.StayTime)
        {
            State = GameState.Defeat;
            return false;
        }

        return true;
    }
}