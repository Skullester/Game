namespace Game;

public interface IGameManager
{
    int Tries { get; }
    GameState State { get; }
    PlayerRole PlayerRole { get; }
    IMaze Maze { get; }
    bool IsGamePlaying { get; }
    void Execute(KeyCommand? command);
    void Initialize();
    void ResetGame();
    void SetVictory();
    void SetDefeat();
    void CheckWallEffect(Effect wallTypeEffect);
}