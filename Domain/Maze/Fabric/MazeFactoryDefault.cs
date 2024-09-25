namespace Models.Fabric;

public class MazeFactoryDefault : MazeFactory
{
    public override string Name => "Обычный";

    public override IRoom GetRoom()
    {
        return new Room();
    }

    public override IWallType GetWallType()
    {
        return new DefaultWallType();
    }
}