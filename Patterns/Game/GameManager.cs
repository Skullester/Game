using Models;
using Models.Fabric;

namespace Game;

public class GameManager
{
    private static GameManager? instance;

    private IMaze? maze;

    public Player2? Player2 { get; set; }

    public IMaze CreateMaze(MazeBuilder builder)
    {
        builder.SetSize();
        builder.GenerateMaze();
        maze = builder.Maze;
        return maze;
    }

    public static GameManager GetManager()
    {
        instance ??= new GameManager();
        return instance;
    }

    public void Start()
    {
    }

    public void RestartGame()
    {
    }
}