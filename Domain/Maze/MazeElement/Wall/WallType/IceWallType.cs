using Models.Maze;

namespace Models.Fabric;

public class IceWallType : WallType
{
    public IceWallType() : base(Effect.Freeze, ConsoleColor.Cyan)
    {
    }
}