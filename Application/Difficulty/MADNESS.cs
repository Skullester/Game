namespace Game;

[Show("Безумный", OrderPriority = 3)]
public class MADNESS : Difficulty
{
    public override string Name => "Безумный";

    public MADNESS() : base(0.3)
    {
    }
}