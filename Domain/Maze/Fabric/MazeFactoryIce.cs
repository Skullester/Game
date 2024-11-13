using Models.Maze;

namespace Models.Fabric;

[Show("Лед")]
public class MazeFactoryIce : MazeFactory
{
    public override IRoom GetRoom()
    {
        return new IceRoom();
    }

    public override WallType GetWallType()
    {
        return new IceWallType();
    }
}