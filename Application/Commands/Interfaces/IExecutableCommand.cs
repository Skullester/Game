namespace Game;

public interface IExecutableCommand : ICommand
{
    void Execute();
}