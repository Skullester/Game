namespace Game;

public interface IDrawingCommand : ICommand
{
    event Action<IEnumerable<Point>>? Drawing;
}