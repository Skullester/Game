namespace Game;

[Show("Средний", OrderPriority = 1)]
public class Medium : Difficulty
{
    public override string Name => "Средний";

    public Medium() : base(0.7)
    {
    }
}