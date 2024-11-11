using System.Diagnostics;
using Game.Extensions;

namespace Game;

[Show("Умение", 'E', Priority = 4)]
public class SkillCommand : Command, IInteractableCommand, IDrawingCommand
{
    public event Action<IEnumerable<Point>>? Drawing;
    private PlayerRole PlayerRole => gm.PlayerRole;
    private readonly IGameManager gm;

    private readonly Stopwatch cdWatch = new Stopwatch();

    public SkillCommand(IGameManager gm)
    {
        this.gm = gm;
    }

    public void Interact() => Execute();

    public void Execute()
    {
        if (!VerifySkillCoolDown()) return;
        /*
        if (PlayerRole is IComplexRole skillPlayer)
        {
            skillPlayer.ComplexSkill.Direction = Direction;
        }
        */

        PlayerRole.UseSkill();
        Drawing?.Invoke(PlayerRole.Skill.View);
    }

    protected override void InitializeSymbols()
    {
        keyMap.Add(ConsoleKey.E);
    }

    private bool VerifySkillCoolDown() => cdWatch.VerifyCondition(cdWatch.Elapsed >= PlayerRole.CoolDownTime);
}