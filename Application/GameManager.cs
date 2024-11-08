using System.Diagnostics;
using Game.Extensions;
using Models.Fabric;

namespace Game;

public sealed class GameManager : IGameManager
{
    public GameState State { get; set; }
    public Player Player { get; }
    public IMaze Maze { get; private set; } = null!;
    public MazeBuilder Builder { get; }
    private Stopwatch stopwatch = null!;

    public GameManager(Player player, MazeBuilder builder)
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

    public bool Execute(Command? command)
    {
        if (command is null || !VerifyTimePenalty()) return false;
        command.Execute();
        return true;
    }

    public void Initialize()
    {
        CreateMaze();
        Player.Initialize();
        State = GameState.Play;
        stopwatch = new Stopwatch();
    }

    private bool VerifyTimePenalty()
    {
        var isVerified = stopwatch.VerifyCondition(stopwatch.Elapsed <= Maze.Room.StayTime);
        if (!isVerified)
            State = GameState.Defeat;
        return isVerified;
    }
}