using System.Drawing;
using Models;

namespace Game;

public class UpCommand : MoveCommand
{
    public override string Name => "Вверх";

    public UpCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, '\u2191', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.W);
        symbolsMap.Add(ConsoleKey.UpArrow);
    }

    public override void Execute() => Execute(new Point(1, 0));
}