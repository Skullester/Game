using UI;

public class GameArtist : IGameArtist
{
    private readonly ConsoleMazeWriter mazeWriter;
    private static GameArtist? instance;

    public GameArtist(ConsoleMazeWriter mazeWriter)
    {
        this.mazeWriter = mazeWriter;
    }

    public void UpdateGameState()
    {
        Console.Clear();
        mazeWriter.Write();
    }

    public static GameArtist GetArtist(ConsoleMazeWriter writer)
    {
        instance ??= new GameArtist(writer);
        return instance;
    }
}

public interface IGameArtist
{
    void UpdateGameState();
}