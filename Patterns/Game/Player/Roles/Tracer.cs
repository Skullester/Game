using Patterns;

namespace ConsoleApp1.Player.Roles;

public class Tracer : Player2
{
    public int MaxTraces { get; private set; }
    public override string Name => "Трейсер";
    public int CurrentTraceCount { get; private set; }

    public Tracer(IMaze maze, int maxTraces) : base(maze)
    {
        MaxTraces = maxTraces;
        CurrentTraceCount = maxTraces;
    }

    public override void UseSkill()
    {
        CurrentTraceCount--;
    }

    public void RestoreSkill()
    {
        CurrentTraceCount = MaxTraces;
    }
}