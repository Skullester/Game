using Models;

namespace Game;

class SkillCommand : Command
{
    private Player player;
    public override string Name => "Умение";

    public SkillCommand(IMaze maze, Player player) : base(maze, 'E')
    {
        this.player = player;
    }

    protected override void InitializeSymbols()
    {
        symbolsMap.Add(ConsoleKey.E);
    }

    public override void Execute()
    {
        var points = player.UseSkill();
        GameArtist.DrawPointsWith(points, "x", player.Color);
    }
}