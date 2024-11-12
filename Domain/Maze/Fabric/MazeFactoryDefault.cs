using Models.Maze;

namespace Models.Fabric;

public class MazeFactoryDefault : MazeFactory
{
    public override string Name => "Обычный";

    public override IRoom GetRoom()
    {
        return new Room();
    }

    public override WallType GetWallType()
    {
        return new DefaultWallType(ConsoleColor.Yellow);
    }
}