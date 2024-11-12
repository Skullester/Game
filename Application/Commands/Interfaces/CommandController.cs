namespace Game;

public class CommandController : IController
{
    public IInteractableCommand? CurrentInteractableCmd { get; set; }
    public IInteractableCommand? CachedInteractableCmd { get; set; }

    public IExecutableCommand Parse(Command? cmd)
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

        return this;
    }

    public CommandController(IInteractableCommand cached)
    {
        CurrentInteractableCmd = CachedInteractableCmd = cached;
    }

    public void Execute()
    {
        CurrentInteractableCmd!.Execute();
    }
}