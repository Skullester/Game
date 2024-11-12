using Models.Maze;

namespace Models.Fabric;

public class MazeFactoryIce : MazeFactory
{
    public override string Name => "Лед";

    public override IRoom GetRoom()
    {
        return new IceRoom();
    }

    public override WallType GetWallType()
    {
        return new IceWallType();
    }
}