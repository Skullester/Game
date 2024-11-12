namespace Game;

[AttributeUsage(AttributeTargets.Class)]
public class ShowAttribute : Attribute
{
    public string Name { get; }
    public char Symbol { get; }
    public uint OrderPriority { get; set; }

    public ShowAttribute(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}