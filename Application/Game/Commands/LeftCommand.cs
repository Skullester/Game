using System.Drawing;
using Models;

namespace Game;

public class LeftCommand : MoveCommand
{
    public override string Name => "Влево";

    public LeftCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, '\u2190', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.A);
        symbolsMap.Add(ConsoleKey.LeftArrow);
    }

    public override void Execute()
    {
        Execute(new Point(0, -1));
    }
}