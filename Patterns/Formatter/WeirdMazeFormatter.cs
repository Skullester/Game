using Models;

namespace UI;

public class WeirdMazeFormatter : MazeFormatter
{
    public override string Name => "Нестандартный";
    public override IReadOnlyDictionary<Type, char> Symbols => charMap.AsReadOnly();

    private readonly Dictionary<Type, char> charMap = new()
        { [typeof(Room)] = '~', [typeof(ExitRoom)] = '$', [typeof(ExternalWall)] = '%', [typeof(InternalWall)] = '@' };
}