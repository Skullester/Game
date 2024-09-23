using UI;

namespace Game;

public abstract class Difficulty : INaming
{
    public abstract string Name { get; }
    public double SkillRatio { get; }

    public Difficulty(double skillRatio)
    {
        SkillRatio = skillRatio;
    }
}