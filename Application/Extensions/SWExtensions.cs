using System.Diagnostics;

namespace Game.Extensions;

public static class SWExtensions
{
    public static bool VerifyCondition(this Stopwatch sw, bool condition)
    {
        if (!IsRunning(sw, true)) return true;
        if (condition) return false;
        sw.Restart();
        return true;
    }

    private static bool IsRunning(Stopwatch sw, bool run)
    {
        if (sw.IsRunning) return true;
        if (run)
            sw.Start();
        return false;
    }
}