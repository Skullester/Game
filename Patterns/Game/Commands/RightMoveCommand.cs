﻿using System.Drawing;
using Models;

namespace Game;

class RightMoveCommand : Command, IMovingCommand
{
    public override string Name => "Вправо";

    public RightMoveCommand(IMaze maze) : base(maze, '\u2192')
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.D);
        symbolsMap.Add(ConsoleKey.RightArrow);
    }

    public override void Execute() => Execute(new Point(0, 1));
}