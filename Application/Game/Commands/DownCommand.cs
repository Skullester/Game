using System.Drawing;
using Models.Maze;
using Models.Player;

namespace Game;

public class DownCommand : MoveCommand
{
    public override string Name => "Вниз";

    public DownCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, '\u2193', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.S);
        symbolsMap.Add(ConsoleKey.DownArrow);
    }

    public override bool Execute() => Execute(new Point(-1, 0));
}