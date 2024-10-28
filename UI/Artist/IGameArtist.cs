namespace UI.Artist;

public interface IGameArtist
{
    IGameManager GameManager { get; }
    IEnumerable<Command> Commands { get; }
    void Initialize();
}