using Models;
using Models.Naming;

// ReSharper disable VirtualMemberCallInConstructor

namespace UI.Displaying;

public abstract class MazeFormatter : INaming
{
    public abstract string Name { get; }
    public IReadOnlyDictionary<Type, char> Symbols => charMap.AsReadOnly();

    protected Dictionary<Type, char> charMap=null!;
    protected abstract void InitializeCharMap();

    protected MazeFormatter()
    {
        InitializeCharMap();
    }

    public char Format(IMazeElement element) => Symbols.GetValueOrDefault(element.GetType(), '?');
}