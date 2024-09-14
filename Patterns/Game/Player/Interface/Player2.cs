using MazePrinter;
using Patterns;

namespace ConsoleApp1;

public abstract class Player2 : INaming
{
    private IMaze maze;
    public abstract string Name { get; }

    public Player2(IMaze maze)
    {
    }

    public abstract void UseSkill();
}