// ReSharper disable VirtualMemberCallInConstructor

namespace Game;

public abstract class KeyCommand(params ConsoleKey[] keys) : ICommand
{
    public IReadOnlyList<ConsoleKey> Keys => keys;
    protected readonly List<ConsoleKey> keys = keys.ToList();
}