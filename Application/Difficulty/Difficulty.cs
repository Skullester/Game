namespace Game;

public abstract class Difficulty
{
    public abstract string Name { get; }
    public double SkillRatio { get; }


    protected Difficulty(double skillRatio)
    {
        SkillRatio = skillRatio;
    }
}