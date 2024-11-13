namespace Infrastucture.Attributes.Visualize;

[AttributeUsage(AttributeTargets.Class)]
public class ShowAttribute : Attribute
{
    public string Name { get; }
    public uint OrderPriority { get; set; }
    public char[]? Symbols { get; set; }

    public ShowAttribute(string name)
    {
        Name = name;
    }
}