using System.Diagnostics;
using Game.Extensions;

namespace Game;

public class SkillCommand : Command
{
    public override string Name => "Умение";

    private readonly Stopwatch cdWatch = new();

    public SkillCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, 'E', gameManager, player)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.E);
    }

    public override bool Execute()
    {
        if (!VerifySkillCoolDown()) return false;
        OnPerfomed(player.GetSkillPoints());
        return false;
    }

    private bool VerifySkillCoolDown() =>
        cdWatch.VerifyCondition(cdWatch.Elapsed < player.CoolDownTime);
}