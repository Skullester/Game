namespace Game;

public class Controller : IExecutableCommand
{
    public IInteractableCommand? interactableCommand { get; set; }

    private readonly MoveCommand moveCommand;

    public Controller(IGameManager gm)
    {
        moveCommand = new MoveCommand(gm);
        interactableCommand = moveCommand;
    }

    public void Execute()
    {
        /*
        var exec = interactableCommand;
        var isExecuted = exec.Interact();
        if (isExecuted)
        {
            interactableCommand = moveCommand;
            // return exec.ShouldGameBeUpdated;
        }
    */
    }
}