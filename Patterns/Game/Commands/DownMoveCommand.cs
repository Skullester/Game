using System.Drawing;
using Models;

namespace Game;

class DownMoveCommand : Command, IMovingCommand
{
    public override string Name => "Вниз";

    public DownMoveCommand(IMaze maze) : base(maze, '\u2193')
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.S);
        symbolsMap.Add(ConsoleKey.DownArrow);
    }

    public override void Execute() => Execute(new Point(-1, 0));
}