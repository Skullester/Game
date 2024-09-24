using Models;
using Models.Fabric;

namespace Game;

public class GameManager
{
    private static GameManager? instance;
    private Command[]? commands;
    public GameState State { get; set; } = GameState.Play;
    public Player? Player2 { get; set; }
    public GameArtist? GameArtist { get; set; }

    public IMaze CreateMaze(MazeBuilder builder)
    {
        builder.SetSize();
        builder.GenerateMaze();
        var maze = builder.Maze;
        commands = GetCommands(maze);
        return maze;
    }

    private Command[] GetCommands(IMaze maze) =>
    [
        new LeftMoveCommand(maze),
        new RightMoveCommand(maze),
        new UpMoveCommand(maze),
        new DownMoveCommand(maze)
    ];

    public static GameManager GetManager()
    {
        instance ??= new GameManager();
        return instance;
    }

    public void Start()
    {
        GameArtist!.UpdateGameState();
        while (State == GameState.Play)
        {
            var cmdChar = Console.ReadKey(true);
            var command = commands!.FirstOrDefault(x => x.SymbolsMap.Contains(cmdChar.Key));
            command?.Execute();
            GameArtist.UpdateGameState();
        }

        switch (State)
        {
            case GameState.Defeat:
                break;
            case GameState.Victory:
                break;
            default:
                break;
        }
    }

    public void RestartGame()
    {
    }
}