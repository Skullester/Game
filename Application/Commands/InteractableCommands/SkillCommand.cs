﻿using System.Diagnostics;
using Game.Extensions;

namespace Game;

[Show("Умение", Symbols = ['E'], OrderPriority = 4)]
public class SkillCommand : KeyCommand, ICommandWithDirection, IDrawingCommand
{
    public event Action<IEnumerable<Point>>? Drawing;
    private PlayerRole PlayerRole => gm.PlayerRole;
    private readonly IGameManager gm;

    private readonly Stopwatch cdWatch = new Stopwatch();
    public Point Direction { get; set; }

    public IController? Controller { get; }

    public SkillCommand(IGameManager gm, IController controller) : base(ConsoleKey.E)
    {
        this.gm = gm;
        Controller = controller;
    }

    public void Execute()
    {
        if (PlayerRole is IComplexRole skillPlayer)
        {
            if (Direction == Point.Empty)
                return;
            skillPlayer.ComplexSkill.Direction = Direction;
        }

        if (!VerifySkillCoolDown())
        {
            ResetState();
            return;
        }

        if (PlayerRole.UseSkill())
            Drawing?.Invoke(PlayerRole.Skill.View);
        ResetState();
    }

    private void ResetState()
    {
        Direction = Point.Empty;
        Controller!.CurrentInteractableCmd = Controller.CachedInteractableCmd;
    }

    private bool VerifySkillCoolDown() => cdWatch.VerifyCondition(cdWatch.Elapsed >= PlayerRole.SkillCooldown);
}