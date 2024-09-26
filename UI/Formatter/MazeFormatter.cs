using Models;
using Models.Naming;

namespace UI.Displaying;

public abstract class MazeFormatter : INaming
{
    public abstract string Name { get; }
    public abstract IReadOnlyDictionary<Type, char> Symbols { get; }

    public char Format(IMazeElement element) => Symbols.GetValueOrDefault(element.GetType(), '?');
}