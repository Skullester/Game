namespace UI.Artist;

public interface IGameArtist
{
    IGameManager GM { get; }
    void Initialize();
}