namespace Game;

public interface IUpdatableCommand
{
    public event Action Updated;
}