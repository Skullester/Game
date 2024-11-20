namespace Game;

public class CommandController : IController
{
    public IInteractablePlayerCommand? CurrentInteractableCmd { get; set; }
    public IInteractablePlayerCommand? CachedInteractableCmd { get; set; }

    public CommandController(IInteractablePlayerCommand cached)
    {
        CurrentInteractableCmd = CachedInteractableCmd = cached;
    }

    public IExecutableCommand Parse(KeyCommand? cmd)
    {
        switch (cmd)
        {
            case ICommandWithDirection interactCmd:
                CurrentInteractableCmd = interactCmd;
                break;
            case IDirection directionObj:
                ((ICommandWithDirection)CurrentInteractableCmd!).Direction = directionObj.Direction;
                break;
            default:
                return (cmd as IExecutableCommand)!;
        }

        return CurrentInteractableCmd;
    }
}