using System.Drawing;
using Models;

namespace Game;

class LeftMoveCommand : Command
{
    public override string Name => "Влево";

    public LeftMoveCommand(IMaze maze) : base(maze)
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.A);
        symbolsMap.Add(ConsoleKey.LeftArrow);
    }

    public override void Execute() => Execute(new Point(0, -1));
}