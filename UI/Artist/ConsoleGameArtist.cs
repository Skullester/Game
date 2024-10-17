using System.Drawing;
using System.Text;
using Models.Player;

// ReSharper disable UnusedMember.Local

namespace UI.Artist;

public class ConsoleGameArtist : IGameArtist
{
    public MazeWriter Writer { get; }
    public IGameManager GameManager { get; }
    public IEnumerable<Command> Commands { get; }
    public ConsoleColor PlayerColor { get; }
    private static ConsoleGameArtist? instance;
    private IMaze maze => GameManager.Maze;

    private Player player => GameManager.Player;
    private string playerCharInStr = null!;
    private const int gameResultTimeout = 1000;

    private ConsoleGameArtist()
    {
        GameManager = GameInitializer.GameManager;
        Writer = GameInitializer.MazeWriter;
        Commands = GameInitializer.Commands;
        PlayerColor = player.Color;
    }

    public void Initialize()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.CursorVisible = false;
        playerCharInStr = player.Name[0].ToString();
        foreach (var command in Commands)
        {
            command.Perfomed += DrawSkillPoints;
        }

        StartGame();
    }

    private void StartGame()
    {
        GameManager.Initialize();
        ConsoleHelper.SetColor(maze.WallType.Color);
        UpdateGameState();
        while (GameManager.State == GameState.Play)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            var cki = Console.ReadKey(true);

            var cmd = Commands.FirstOrDefault(x => x.KeyMap.Contains(cki.Key));

            if (GameManager.Execute(cmd) && cmd!.IsGameShouldUpdated)
            {
                UpdateGameState();
            }
        }

        CheckState();
    }

    public void CheckState()
    {
        switch (GameManager.State)
        {
            case GameState.Reset:
                StartGame();
                break;
            case GameState.Defeat:
                DrawDefeat();
                StartGame();
                break;
            case GameState.Victory:
                DrawVictory();
                break;
        }
    }


    private static class CursorPositionContainer
    {
        private static int left;
        private static int top;

        public static void Save()
        {
            left = Console.CursorLeft;
            top = Console.CursorTop;
        }

        public static (int, int) Get() => (left, top);

        public static void Set()
        {
            Console.SetCursorPosition(left, top);
        }
    }

    private void DrawInstructions()
    {
        CursorPositionContainer.Save();
        var foregroundColor = Console.ForegroundColor;
        ConsoleHelper.SetColor(ConsoleColor.Yellow);
        var i = 0;
        const int offset = 2;
        foreach (var cmd in Commands)
        {
            Console.SetCursorPosition(maze.Width + offset, i++);
            ConsoleHelper.PrintLine(($"\"{cmd.Symbol}\" - {cmd.Name}"));
        }

        CursorPositionContainer.Set();
        ConsoleHelper.SetColor(foregroundColor);
    }

    private void DrawPlayer()
    {
        CursorPositionContainer.Save();
        DrawPoint(player.Location, playerCharInStr, PlayerColor);
        CursorPositionContainer.Set();
    }

    private void UpdateGameState()
    {
        Console.Clear();
        Writer.Write();
        DrawInstructions();
        DrawPlayer();
    }

    public static ConsoleGameArtist GetArtist()
    {
        instance ??= new ConsoleGameArtist();
        return instance;
    }

    private void DrawVictory()
    {
        DrawGameResult("Победа!", ConsoleColor.Green);
    }

    private void DrawDefeat()
    {
        DrawGameResult("Поражение!", ConsoleColor.White);
    }

    private void DrawGameResult(string name, ConsoleColor color)
    {
        ConsoleHelper.PrintWithColor(name, color);
        Thread.Sleep(gameResultTimeout);
    }

    private void DrawSkillPoints(IEnumerable<Point> points)
    {
        foreach (var point in points.Where(x => x != player.Location))
        {
            DrawPoint(point, "x", player.Color);
            Thread.Sleep(1);
        }
    }

    private void DrawPoint(Point point, string text, ConsoleColor color)
    {
        Console.SetCursorPosition(point.Y, point.X);
        ConsoleHelper.PrintWithColor(text, color);
    }
}