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
        var isExecuted = command.Execute();
        CheckTimePenalty();
        return isExecuted;
    }

    public void Initialize()
    {
        CreateMaze();
        Player.Initialize();
        State = GameState.Play;
    }

    private async void CheckTimePenalty()
    {
        var mazePlayerPoint = Player.Location;
        await Task.Delay(Maze.Room.StayTime);
        if (Player.Location == mazePlayerPoint)
            State = GameState.Defeat;
    }
}