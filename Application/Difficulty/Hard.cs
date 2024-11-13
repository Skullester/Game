namespace Game;

[Show("Сложный", OrderPriority = 2)]
public class Hard : Difficulty
{
    public override string Name => "Сложный";

    public Hard() : base(0.5)
    {
    }
}