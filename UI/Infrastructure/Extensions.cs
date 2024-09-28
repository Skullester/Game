using Models;

namespace Infrastructure;

public static class Extensions
{
    public static IEnumerable<char> ParseToChar(this IEnumerable<IMazeElement> elements, MazeFormatter formatter)
    {
        return elements.Select(formatter.Format);
    }
}