namespace Models.Maze;

public class DefaultWallType : WallType
{
    public DefaultWallType(ConsoleColor color) : base(State.None,ConsoleColor.DarkCyan)
    {
    }
}