using System.Diagnostics;
using Game.Extensions;

namespace Game;

public class SkillCommand : Command
{
    private readonly Player player;
    public override string Name => "Умение";

    private readonly Stopwatch cdWatch = new Stopwatch();

    public SkillCommand(Player player) : base('E', true)
    {
        this.player = player;
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.E);
    }

    public override void Execute()
    {
        if (!VerifySkillCoolDown()) return;
        // OnPerfomed(player.GetSkillPoints());
    }

    private bool VerifySkillCoolDown() =>
        cdWatch.VerifyCondition(cdWatch.Elapsed >= player.CoolDownTime);
}