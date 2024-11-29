using Models.Maze;

namespace Models.Fabric;

[Show("Лед")]
public class MazeFactoryIce : MazeFactory
{
    public override IRoom GetRoom() => new IceRoom();

    public override WallType GetExWallType() => new IceWallType(Effect.None, ConsoleColor.White);

    public override WallType GetInWallType() => new IceWallType(Effect.Freeze, ConsoleColor.Cyan);
}