namespace UI.Displaying;

public class WeirdMazeFormatter : MazeFormatter
{
    public override string Name => "Нестандартный";

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