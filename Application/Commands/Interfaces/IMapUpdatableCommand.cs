namespace Game;

public interface IMapUpdatableCommand : ICommand
{
    public event Action Updated;
}