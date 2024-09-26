using Models.Fabric;
using Models.Maze;
using Models.Player;

namespace Game;

public interface IGameManager
{
    GameState State { get; set; }
    Player Player { get; }
    IMaze Maze { get; }
    MazeBuilder Builder { get; }
    bool Execute(Command command);
    void Initialize();
}