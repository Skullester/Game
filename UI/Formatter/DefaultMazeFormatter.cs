namespace UI.Displaying;

public class DefaultMazeFormatter : MazeFormatter
{
    public override string Name => "Стандартный";

    protected override void InitializeCharMap()
    {
        charMap = new()
        {
            [typeof(Room)] = ' ', [typeof(FireRoom)] = ' ', [typeof(ExitRoom)] = 'Q', [typeof(ExternalWall)] = '#',
            [typeof(InternalWall)] = '*',
        };
    }
}