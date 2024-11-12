using Models.Maze;

namespace Models.Fabric;

public class IceWallType : WallType
{
    public IceWallType() : base(Maze.Effect.Freeze, ConsoleColor.Cyan)
    {
    }
}