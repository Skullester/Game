using System.Diagnostics;
using Models.Maze;
using Models.Player;

namespace Game;

public class SkillCommand : Command
{
    public override string Name => "Умение";

    public SkillCommand(IMaze maze, IGameManager gameManager, Player player) : base(maze, 'E', gameManager, player)
    {
    }

    private readonly Stopwatch coolDownStopwatch = new();

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

    private bool VerifySkillCoolDown()
    {
        if (!coolDownStopwatch.IsRunning)
        {
            coolDownStopwatch.Start();
            return true;
        }

        if (coolDownStopwatch.Elapsed < player.CoolDownTime)
            return false;
        coolDownStopwatch.Restart();
        return true;
    }
}