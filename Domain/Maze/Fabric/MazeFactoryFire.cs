using Models.Maze;

namespace Models.Fabric;

[Show("Огненный")]
public class MazeFactoryFire : MazeFactory
{
    public override IRoom GetRoom() => new FireRoom();

    public override WallType GetExWallType() => new FireWallType(Effect.None, ConsoleColor.White);

    public override WallType GetInWallType() => new FireWallType(Effect.Death, ConsoleColor.Red);
}