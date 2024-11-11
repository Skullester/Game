namespace Game;

public interface IDrawingCommand
{
    event Action<IEnumerable<Point>>? Drawing;
}