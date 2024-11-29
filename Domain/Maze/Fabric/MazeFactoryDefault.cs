using Models.Maze;

namespace Models.Fabric;

[Show("Обычный")]
public class MazeFactoryDefault : MazeFactory
{
    public override IRoom GetRoom()
    {
        return new Room();
    }

    public override WallType GetExWallType()
    {
        return new DefaultWallType(ConsoleColor.White);
    }

    public override WallType GetInWallType()
    {
        return new DefaultWallType(ConsoleColor.Yellow);
    }
}