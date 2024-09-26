using Game;
using UI.Displaying;

namespace UI.Artist;

public interface IGameArtist
{
    MazeWriter Writer { get; }
    IGameManager GameManager { get; }
    IEnumerable<Command> Commands { get; }
    void Initialize();
}