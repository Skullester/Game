using System.Text;
using UI.Artist;

namespace UI;

public class Program
{
    private static void Main()
    {
        GameInitializer.Start();
        IGameArtist gameArtist = ConsoleGameArtist.GetArtist(ConsoleColor.Blue);
        gameArtist.Initialize();
    }
}