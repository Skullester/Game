using System.Drawing;
using System.Text;
using Extensions;
using Models.Player;
using static Game.ConsoleHelper;

// ReSharper disable UnusedMember.Local

namespace UI.Artist;

public class ConsoleGameArtist : IGameArtist
{
    public MazeWriter Writer { get; }
    public IGameManager GM { get; }
    public IEnumerable<ICommand> Commands { get; }
    private readonly KeyCommand[] keyCommands;
    private readonly ShowAttribute[] showAttributeCommands;
    private ConsoleColor playerColor => PlayerRole.Color;
    private IMaze maze => GM.Maze;
    private PlayerRole PlayerRole => GM.PlayerRole;
    private string playerCharInStr = null!;
    private const int gameResultTimeout = 1000;
    private const ConsoleColor instructionColor = ConsoleColor.Yellow;
    private const ConsoleColor victoryColor = ConsoleColor.Green;
    private const ConsoleColor defeatColor = ConsoleColor.Red;

    public ConsoleGameArtist(IGameManager manager, MazeWriter writer, IEnumerable<ICommand> commands)
    {
        Writer = writer;
        Commands = commands.ToArray();
        GM = manager;
        keyCommands = Commands.OfType<KeyCommand>()
            .ToArray();
        showAttributeCommands = Commands.GetShowAttributes(true)
            .ToArray();
    }

    public void Initialize()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.CursorVisible = false;
        InitializePlayerChar();
        foreach (var command in Commands.OfType<IDrawingCommand>())
        {
            command.Drawing += Draw;
        }

        foreach (var command in Commands.OfType<IMapUpdatableCommand>())
        {
            command.Updated += UpdateGameState;
        }

        StartGame();
    }

    private void InitializePlayerChar()
    {
        var showAttribute = PlayerRole.GetShowAttribute()!;
        playerCharInStr = showAttribute.Name[0]
            .ToString();
    }

    private void StartGame()
    {
        GM.Initialize();
        UpdateGameState();
        while (GM.IsGamePlaying)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            var cki = Console.ReadKey(true);
            var cmd = keyCommands.FirstOrDefault(x => x.Keys.Contains(cki.Key));
            GM.Execute(cmd);
        }

        CheckState();
    }

    public void CheckState()
    {
        switch (GM.State)
        {
            case GameState.Reset:
                StartGame();
                break;
            case GameState.Defeat:
                DrawDefeat();
                StartGame();
                break;
            case GameState.FullDefeat:
                DrawFullDefeat();
                break;
            case GameState.Victory:
                DrawVictory();
                break;
        }
    }

    private void DrawInstructions()
    {
        CursorPositionContainer.Save();
        var foregroundColor = Console.ForegroundColor;
        SetColor(instructionColor);
        var i = 0;
        const int offset = 2;
        var leftOffset = maze.Width + offset;
        Console.SetCursorPosition(leftOffset, i);
        if (GM.Maze.Room.StayTime != TimeSpan.MaxValue)
        {
            PrintLine("Количество попыток: " + GM.Tries);
            i++;
        }

        foreach (var attribute in showAttributeCommands)
        {
            Console.SetCursorPosition(leftOffset, i++);
            var text = $"\"{string.Join('|', attribute.Symbols!)}\" - {attribute.Name}";
            if (attribute.Name == "Умение")
            {
                text += $"({PlayerRole.Skill.RemainingUses})";
            }

            PrintLine(text);
        }

        CursorPositionContainer.Set();
        SetColor(foregroundColor);
    }

    private void DrawPlayer()
    {
        CursorPositionContainer.Save();
        DrawPoint(PlayerRole.Location, playerCharInStr, playerColor);
        CursorPositionContainer.Set();
    }

    private void UpdateGameState()
    {
        WriteMap();
        DrawInstructions();
        DrawPlayer();
    }

    private void DrawFullDefeat()
    {
        DrawGameResult("Полное поражение!", defeatColor);
        Dispose();
    }

    private void Dispose()
    {
        (Writer as IDisposable).Dispose();
    }

    private void DrawVictory()
    {
        DrawGameResult("Победа!", victoryColor);
        Dispose();
    }

    private void DrawDefeat()
    {
        DrawGameResult("Поражение!", defeatColor);
    }

    private void DrawGameResult(string name, ConsoleColor color)
    {
        UpdateGameState();
        PrintWithColor(name, color);
        Thread.Sleep(gameResultTimeout);
    }

    private void WriteMap()
    {
        Console.Clear();
        Writer.Write();
    }

    private void Draw(IEnumerable<Point> points)
    {
        foreach (var point in points.Where(x => x != PlayerRole.Location))
        {
            DrawPoint(point, "x", PlayerRole.Color);
            Thread.Sleep(1);
        }
    }

    private void DrawPoint(Point point, string text, ConsoleColor color)
    {
        Console.SetCursorPosition(point.Y, point.X);
        PrintWithColor(text, color);
    }
}