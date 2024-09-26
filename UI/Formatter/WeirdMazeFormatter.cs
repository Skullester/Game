using Models.Maze;

namespace UI.Displaying;

public class WeirdMazeFormatter : MazeFormatter
{
    public override string Name => "Нестандартный";
    public override IReadOnlyDictionary<Type, char> Symbols => charMap.AsReadOnly();

    private readonly Dictionary<Type, char> charMap = new()
    {
        [typeof(Room)] = '~', [typeof(FireRoom)] = '~', [typeof(ExitRoom)] = '$', [typeof(ExternalWall)] = '%',
        [typeof(InternalWall)] = '@'
    };
}