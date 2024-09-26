using System.Drawing;
using Models.Maze;
using Models.Player;

namespace Game;

public class RightCommand : MoveCommand
{
    public override string Name => "Вправо";

    public RightCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, '\u2192', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.D);
        keyMap.Add(ConsoleKey.RightArrow);
    }

    public override bool Execute() => Execute(new Point(0, 1));
}