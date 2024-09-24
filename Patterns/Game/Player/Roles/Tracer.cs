namespace Models;

public class Tracer : Player
{
    public const int MaxTraces = 8;
    public int CurrentTraceCount { get; private set; }
    public override string Name => "Трейсер";

    public Tracer(IMaze maze, int maxTraces) : base(maze)
    {
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