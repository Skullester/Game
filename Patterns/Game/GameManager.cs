using Models;
using Models.Fabric;

namespace Game;

public class GameManager
{
    private static GameManager? instance;
    public event Action Perfomed;
    public IReadOnlyCollection<Command> Commands => commands.AsReadOnly();
    private Command[]? commands;
    public GameState State { get; set; } = GameState.Play;
    public Player Player { get; set; } = null!;
    public GameArtist GameArtist { get; set; } = null!;
    public IMaze Maze { get; private set; }

    public IMaze CreateMaze(MazeBuilder builder)
    {
        builder.SetSize();
        builder.GenerateMaze();
        Maze = builder.Maze;
        commands = GetCommands(Maze);
        return Maze;
    }

    private Command[] GetCommands(IMaze? maze) =>
    [
        new LeftMoveCommand(maze),
        new RightMoveCommand(maze),
        new UpMoveCommand(maze),
        new DownMoveCommand(maze),
        new SkillCommand(maze, Player),
        new RestartCommand(maze)
    ];

    public static GameManager GetManager()
    {
        instance ??= new GameManager();
        return instance;
    }

    public void Start()
    {
        Player.Initialize();
        GameArtist.StartGame();
        while (State == GameState.Play)
        {
            var cmdChar = Console.ReadKey(true);
            var command = commands!.FirstOrDefault(x => x.SymbolsMap.Contains(cmdChar.Key));
            if (command is IMovingCommand)
            {
                Perfomed?.Invoke();
            }

            command?.Execute();
            GameArtist.UpdateGameState();
        }

        CheckState();
    }

    public void CheckState()
    {
        switch (State)
        {
            case GameState.Reset:
                RestartGame();
                break;
            case GameState.Defeat:
                GameArtist.DrawDefeat();
                RestartGame();
                break;
            case GameState.Victory:
                GameArtist.DrawVictory();
                break;
        }
    }

    public void RestartGame()
    {
        Maze.Generate();
        Player.SetDefaultValues();
        State = GameState.Play;
        Start();
    }
}