using System.Diagnostics;
using Game.Extensions;

namespace Game;

public class SkillCommand : Command
{
    public override string Name => "Умение";

    private readonly Stopwatch cdWatch = new Stopwatch();

    public SkillCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, 'E', gameManager, player,
        true)
    {
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.E);
    }

    public override void Execute()
    {
        if (!VerifySkillCoolDown()) return;
        OnPerfomed(player.GetSkillPoints());
    }

    private bool VerifySkillCoolDown() =>
        cdWatch.VerifyCondition(cdWatch.Elapsed >= player.CoolDownTime);
}