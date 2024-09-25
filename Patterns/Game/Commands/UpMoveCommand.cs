using System.Drawing;
using Models;

namespace Game;

class UpMoveCommand : Command, IMovingCommand
{
    public override string Name => "Вверх";

    public UpMoveCommand(IMaze maze) : base(maze, '\u2191')
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.W);
        symbolsMap.Add(ConsoleKey.UpArrow);
    }

    public override void Execute() => Execute(new Point(1, 0));
}