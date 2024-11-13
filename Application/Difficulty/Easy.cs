namespace Game;

[Show("Легкий", OrderPriority = 0)]
public class Easy : Difficulty
{
    public override string Name => "Легкий";

    public Easy() : base(1)
    {
    }
}