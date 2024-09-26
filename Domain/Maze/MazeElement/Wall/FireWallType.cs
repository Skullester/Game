namespace Models.Maze;
public class FireWallType : WallType
{
    public FireWallType() : base(State.Death, ConsoleColor.Red)
    {
    }
}