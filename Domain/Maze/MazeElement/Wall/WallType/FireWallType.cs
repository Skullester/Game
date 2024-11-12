namespace Models.Maze;

public class FireWallType : WallType
{
    public FireWallType() : base(Effect.Death, ConsoleColor.Red)
    {
    }
}