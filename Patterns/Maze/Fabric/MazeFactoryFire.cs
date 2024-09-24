namespace Models.Fabric;

public class MazeFactoryFire : MazeFactory
{
    public override string Name => "Огненный";

    public override IRoom GetRoom()
    {
        return new FireRoom();
    }

    public override IWallType GetWallType()
    {
        return new FireWallType();
    }
}