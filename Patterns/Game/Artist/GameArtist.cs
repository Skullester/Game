using System.Drawing;
using Game;
using Models;
using UI;

public class GameArtist : IGameArtist
{
    private readonly ConsoleMazeWriter mazeWriter;
    private static GameArtist? instance;
    private readonly GameManager gameManager;
    private IMaze? maze => gameManager.Maze;

    public GameArtist(ConsoleMazeWriter mazeWriter)
    {
        this.mazeWriter = mazeWriter;
        gameManager = GameManager.GetManager();
    }

    public void StartGame()
    {
        ConsoleHelper.SetConsoleColor(maze.WallType.Color);
        Console.CursorVisible = false;
        UpdateGameState();
        mazeWriter.Delay = 0;
    }

    private void DrawInstructions()
    {
        var left = Console.CursorLeft;
        var top = Console.CursorTop;
        var foregroundColor = Console.ForegroundColor;
        ConsoleHelper.SetConsoleColor(ConsoleColor.Yellow);
        var i = 0;
        var offset = 2;
        foreach (var cmd in gameManager.Commands)
        {
            Console.SetCursorPosition(maze.Width + offset, i++);
            Console.WriteLine($"\"{cmd.Symbol}\" - {cmd.Name}");
        }

        Console.SetCursorPosition(left, top);
        ConsoleHelper.SetConsoleColor(foregroundColor);
    }

    public void DrawPlayer(char playerChar, Point playerPoint)
    {
        var left = Console.CursorLeft;
        var top = Console.CursorTop;
        DrawPoint(playerPoint, playerChar.ToString(), ConsoleColor.Yellow);
        Console.SetCursorPosition(left, top);
    }

    public void UpdateGameState()
    {
        Console.Clear();
        mazeWriter.Write();
        DrawInstructions();
        DrawPlayer(gameManager.Player.Name[0], maze.PlayerPoint);
        CheckTimePenalty();
    }

    public async Task CheckTimePenalty()
    {
        var mazePlayerPoint = maze.PlayerPoint;
        await Task.Delay(maze.Room.StayTime);
        if (maze.PlayerPoint == mazePlayerPoint)
            gameManager.State = GameState.Defeat;
    }


    public static GameArtist GetArtist(ConsoleMazeWriter writer)
    {
        instance ??= new GameArtist(writer);
        return instance;
    }

    public void DrawVictory()
    {
        DrawGameResult("Победа!", ConsoleColor.Green);
    }

    public void DrawDefeat()
    {
        DrawGameResult("Поражение!", ConsoleColor.White);
    }

    private void DrawGameResult(string name, ConsoleColor color)
    {
        ConsoleHelper.PrintWithColor(name, color);
        Thread.Sleep(1000);
    }

    public static void DrawPointsWith(IEnumerable<Point> points, string text, ConsoleColor color)
    {
        foreach (var point in points)
        {
            DrawPoint(point, text, color);
            Thread.Sleep(50);
        }
    }

    public static void DrawPoint(Point point, string text, ConsoleColor color)
    {
        Console.SetCursorPosition(point.Y, point.X);
        ConsoleHelper.PrintWithColor(text, color);
    }
}