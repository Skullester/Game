using Models.Maze;

namespace Models.Fabric;

[Show("Обычный")]
public class MazeFactoryDefault : MazeFactory
{
    public override IRoom GetRoom()
    {
        return new Room();
    }

    public override WallType GetWallType()
    {
        return new DefaultWallType(ConsoleColor.Yellow);
    }
}