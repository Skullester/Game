namespace Game;

public abstract class DirectionCommand : Command
{
    private MoveCommand command;

    protected DirectionCommand(char symbol, MoveCommand command) : base(symbol, true)
    {
        this.command = command;
    }

    protected abstract Point GetDirection();

    public override void Execute()
    {
        command.Execute(GetDirection());
    }
}