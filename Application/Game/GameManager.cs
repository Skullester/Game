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
    public GameArtist Artist { get; set; } = null!;
    public IMaze? Maze { get; private set; }

    public IMaze? CreateMaze(MazeBuilder builder)
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
        Artist.StartGame();
        while (State == GameState.Play)
        {
            // int i = 0;
            while (Console.KeyAvailable)
            {
                // i++;
                Console.ReadKey(true);
            }

            // Console.WriteLine(i);
            var cmdChar = Console.ReadKey(true);
            var command = commands!.FirstOrDefault(x => x.SymbolsMap.Contains(cmdChar.Key));
            if (command is null) continue;
            if (command is IMovingCommand)
            {
                Perfomed?.Invoke();
            }

            command.Execute();
            Artist.UpdateGameState();
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
                Artist.DrawDefeat();
                RestartGame();
                break;
            case GameState.Victory:
                Artist.DrawVictory();
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