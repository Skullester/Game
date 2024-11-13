namespace Game;

public abstract class Difficulty
{
    public double SkillRatio { get; }


    protected Difficulty(double skillRatio)
    {
        SkillRatio = skillRatio;
    }
}