using Models;

namespace Extensions;

public static class MazeEx
{
    public static IEnumerable<(char sym, IMazeElement el)> ParseToTuple(this IEnumerable<IMazeElement> elements,
        MazeFormatter formatter)
    {
        return elements.Select(x => (formatter.Format(x), x));
    }
}