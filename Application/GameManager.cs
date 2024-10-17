using System.Diagnostics;
using Game.Extensions;
using Models.Fabric;

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