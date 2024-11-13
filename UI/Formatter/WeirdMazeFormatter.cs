namespace UI.Displaying;

[Show("Нестандартный", Symbols = ['~', '$', '%', '@'])]
public class WeirdMazeFormatter : MazeFormatter
{
    protected override void InitializeCharMap()
    {
        charMap = new Dictionary<Type, char>
        {
            [typeof(Room)] = '~',
            [typeof(FireRoom)] = '~',
            [typeof(ExitRoom)] = '$',
            [typeof(ExternalWall)] = '%',
            [typeof(InternalWall)] = '@'
        };
    }
}