using Models;

namespace UI;

public interface IMazeFormatter : INaming
{
    IReadOnlyList<char> Symbols { get; }
    char Format(IMazeElement element);
}