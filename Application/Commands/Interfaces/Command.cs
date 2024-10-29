// ReSharper disable VirtualMemberCallInConstructor

namespace Game;

public abstract class Command : INaming
{
    public event Action<IEnumerable<Point>>? Perfomed;
    public bool ShouldGameBeUpdated { get; }
    public IReadOnlySet<ConsoleKey> KeyMap => keyMap;
    public char Symbol { get; }
    public abstract string Name { get; }
    protected readonly HashSet<ConsoleKey> keyMap = new HashSet<ConsoleKey>();

    protected Command(char symbol, bool shouldGameBeUpdated)
    {
        Symbol = symbol;
        ShouldGameBeUpdated = shouldGameBeUpdated;
        InitializeSymbols();
    }

    protected abstract void InitializeSymbols();
    public abstract void Execute();

    protected virtual void OnPerfomed(IEnumerable<Point> obj)
    {
        Perfomed?.Invoke(obj);
    }
}