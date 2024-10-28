﻿namespace Game;

public class LeftCommand : MoveCommand
{
    public override string Name => "Влево";

    public LeftCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, '\u2190', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.A);
        keyMap.Add(ConsoleKey.LeftArrow);
    }

    protected override Point GetDirection() => new Point(0, -1);
}