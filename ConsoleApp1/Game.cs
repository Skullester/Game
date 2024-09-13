namespace ConsoleApp1;

public class Game
{
    private static readonly Game game = new Game();
    
    private Game()
    {
    }

    public static Game GetGame() => game;
}