using Models.Maze;

namespace Models.Fabric;

public class MazeFactoryFire : MazeFactory
{
    public override string Name => "Огненный";

    public override IRoom GetRoom()
    {
        return new FireRoom();
    }

    public override WallType GetWallType()
    {
        return new FireWallType();
    }
}