using Patterns;

namespace ConsoleApp1;

public class Game
{
    private static Game? game;
    private IMaze maze;
    public Player2 Player2 { get; }

    private Game(Player2 player, IMaze maze)
    {
        this.maze = maze;
        Player2 = player;
    }

    public static Game GetGame(Player2 player, IMaze maze)
    {
        game ??= new Game(player, maze);
        return game;
    }
}