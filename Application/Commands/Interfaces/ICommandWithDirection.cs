namespace Game;

public interface ICommandWithDirection : IInteractableCommand
{
    public Point Direction { get; set; }
}