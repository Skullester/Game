// ReSharper disable VirtualMemberCallInConstructor

namespace Game;

public abstract class KeyCommand : ICommand
{
    public IReadOnlySet<ConsoleKey> KeyMap => keyMap;
    protected readonly HashSet<ConsoleKey> keyMap;

    protected KeyCommand(params ConsoleKey[] keys)
    {
        keyMap = new HashSet<ConsoleKey>(keys);
    }
}