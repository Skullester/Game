using System.Drawing;
using Models;

namespace Game;

class DownMoveCommand : Command
{
    public override string Name => "Вниз";

    public DownMoveCommand(IMaze maze) : base(maze)
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.S);
        symbolsMap.Add(ConsoleKey.DownArrow);
    }

    public override void Execute() => Execute(new Point(-1, 0));
}