using UI.Artist;

namespace UI;

public class Program
{
    private static void Main()
    {
        GameInitializer.Start();
        var gameArtist = ConsoleGameArtist.GetArtist();
        gameArtist.Initialize();
    }
}