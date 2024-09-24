using System.Drawing;
using Models;

namespace Game;

class RightMoveCommand : Command
{
    public override string Name => "Вправо";

    public RightMoveCommand(IMaze maze) : base(maze)
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.D);
        symbolsMap.Add(ConsoleKey.RightArrow);
    }

    public override void Execute() => Execute(new Point(0, 1));
}