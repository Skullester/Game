namespace Game;

public interface ICommandWithDirection : IInteractablePlayerCommand
{
    public Point Direction { get; set; }
}