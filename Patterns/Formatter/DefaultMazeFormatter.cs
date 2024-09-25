using Models;

namespace UI;

public class DefaultMazeFormatter : MazeFormatter
{
    public override string Name => "Стандартный";
    public override IReadOnlyDictionary<Type, char> Symbols => charMap.AsReadOnly();

    private readonly Dictionary<Type, char> charMap = new()
    {
        [typeof(Room)] = ' ', [typeof(FireRoom)] = ' ', [typeof(ExitRoom)] = 'Q', [typeof(ExternalWall)] = '#',
        [typeof(InternalWall)] = '*',
    };
}