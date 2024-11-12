using System.Diagnostics;
using Game.Extensions;
using Models.Fabric;

namespace Game;

public sealed class GameManager : IGameManager
{
    public GameState State { get; private set; }
    public PlayerRole PlayerRole { get; }
    public IMaze Maze { get; private set; } = null!;
    public bool IsGameFinished => State != GameState.Play;
    public MazeBuilder Builder { get; }
    private Stopwatch stopwatch = null!;
    private IController controller => lazyController.Value;
    private readonly Lazy<IController> lazyController;

    public GameManager(PlayerRole playerRole, MazeBuilder builder, Lazy<IController> controller)
    {
        lazyController = controller;
        PlayerRole = playerRole;
        Builder = builder;
    }

    private void CreateMaze()
    {
        Builder.SetSize();
        Builder.GenerateMaze();
        Maze = Builder.Maze;
    }

    public void Execute(Command? cmd)
    {
        var isVerified = cmd != null && VerifyTimePenalty();
        if (!isVerified) return;
        var executable = controller.Parse(cmd);
        executable.Execute();
    }

    public void Initialize()
    {
        CreateMaze();
        PlayerRole.Initialize();
        State = GameState.Play;
        stopwatch = new Stopwatch();
    }

    public void CheckEffect(Effect wallTypeEffect)
    {
        switch (wallTypeEffect)
        {
            case Effect.Death:
            {
                SetDefeat();
                break;
            }
            case Effect.Freeze:
            {
                Freeze();
                break;
            }
        }
    }

    private void Freeze()
    {
        Thread.Sleep(TimeSpan.FromSeconds(2));
    }

    public void ResetGame() => SetState(GameState.Reset);

    public void SetVictory() => SetState(GameState.Victory);

    public void SetDefeat() => SetState(GameState.Defeat);

    private void SetState(GameState state) => State = state;

    private bool VerifyTimePenalty()
    {
        var isVerified = stopwatch.VerifyCondition(stopwatch.Elapsed <= Maze.Room.StayTime);
        if (!isVerified)
            State = GameState.Defeat;
        return isVerified;
    }
}