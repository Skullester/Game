// ReSharper disable VirtualMemberCallInConstructor

namespace Game;

public abstract class Command
{
    public IReadOnlySet<ConsoleKey> KeyMap => keyMap;
    protected readonly HashSet<ConsoleKey> keyMap = new HashSet<ConsoleKey>();

    protected Command()
    {
        InitializeSymbols();
    }

    protected virtual void InitializeSymbols()
    {
    }
}