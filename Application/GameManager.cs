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
    private readonly Lazy<Controller> controller;

    public GameManager(PlayerRole playerRole, MazeBuilder builder, Lazy<Controller> controller)
    {
        this.controller = controller;
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
        var executable = GetExecCmd(cmd!);
        executable.Execute();
        // return verifyTimePenalty && cmd.Execute();
    }

    private IExecutableCommand GetExecCmd(Command cmd)
    {
        if (cmd is IInteractableCommand interactableCommand)
        {
        }

        if (cmd is IExecutableCommand cmde)
            return cmde;
        return null;
    }

    /*private IExecutableCommand ValidateCommand(Command? cmd, Empty empty)
    {
        if (cmd is IInteractableCommand dirCmd)
        {
            empty.interactableCommand = dirCmd;
        }
        else
        {
            var dir = GetDirection(cmd);
            (empty.interactableCommand! as ICommandWithDirection)!.Direction = dir;
        }

        if (PlayerRole is IComplexRole cp)
        {
            cp.ComplexSkill.Direction
        }
    }*/

    public void Initialize()
    {
        CreateMaze();
        PlayerRole.Initialize();
        State = GameState.Play;
        stopwatch = new Stopwatch();
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