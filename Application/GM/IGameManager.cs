namespace Game;

public interface IGameManager
{
    GameState State { get; }
    PlayerRole PlayerRole { get; }
    IMaze Maze { get; }
    bool IsGameFinished { get; }
    void Execute(Command? command);
    void Initialize();
    void ResetGame();
    void SetVictory();
    void SetDefeat();
    void CheckEffect(Effect wallTypeEffect);
}