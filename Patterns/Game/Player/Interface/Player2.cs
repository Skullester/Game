using UI;

namespace Models;

public abstract class Player2 : INaming
{
    private IMaze maze;
    public abstract string Name { get; }

    public Player2(IMaze maze)
    {
    }

    public abstract void UseSkill();
}