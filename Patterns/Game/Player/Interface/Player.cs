using Patterns.Naming;

namespace Models;

public abstract class Player : INaming
{
    private IMaze maze;
    public abstract string Name { get; }

    public Player(IMaze maze)
    {
    }

    public abstract void UseSkill();
}