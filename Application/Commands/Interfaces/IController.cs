namespace Game;

public interface IController : IExecutableCommand
{
    IInteractableCommand? CurrentInteractableCmd { get; set; }
    IInteractableCommand? CachedInteractableCmd { get; set; }
    IExecutableCommand Parse(Command? cmd);
}