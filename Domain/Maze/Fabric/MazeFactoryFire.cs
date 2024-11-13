using Models.Maze;

namespace Models.Fabric;

[Show("Огненный")]
public class MazeFactoryFire : MazeFactory
{
    public override IRoom GetRoom()
    {
        return new FireRoom();
    }

    public override WallType GetWallType()
    {
        return new FireWallType();
    }
}