using UI.Artist;

namespace UI;

public class Program
{
    private static void Main()
    {
        GameInitializer.Start();
        IGameArtist gameArtist = ConsoleGameArtist.GetArtist(ConsoleColor.Yellow);
        gameArtist.Initialize();
        var totalMemory = GC.GetTotalMemory(true);
        Console.WriteLine(totalMemory);
    }
}