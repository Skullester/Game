﻿using Models.Maze;
using Models.Player;

namespace Game;

public class SkillCommand : Command
{
    public override string Name => "Умение";

    public SkillCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, 'E', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.E);
    }

    public override bool Execute()
    {
        OnPerfomed(player.GetSkillPoints());
        return false;
    }
}