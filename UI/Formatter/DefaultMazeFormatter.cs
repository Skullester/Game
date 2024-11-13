using Models.Fabric;

namespace UI.Displaying;

[Show("Стандартный", Symbols = [' ', 'Q', '#', '*'])]
public class DefaultMazeFormatter : MazeFormatter
{
    protected override void InitializeCharMap()
    {
        charMap = new Dictionary<Type, char>
        {
            [typeof(Room)] = ' ', [typeof(FireRoom)] = ' ', [typeof(ExitRoom)] = 'Q', [typeof(ExternalWall)] = '#',
            [typeof(InternalWall)] = '*', [typeof(IceRoom)] = ' '
        };
    }
}