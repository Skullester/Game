namespace Game;

public interface IController
{
    IInteractablePlayerCommand? CurrentInteractableCmd { get; set; }
    IInteractablePlayerCommand? CachedInteractableCmd { get; set; }
    IExecutableCommand Parse(KeyCommand? cmd);
}