using Models.Maze;

namespace Models.Fabric;

public abstract class MazeFactory
{
    public abstract IRoom GetRoom();
    public abstract WallType GetExWallType();
    public abstract WallType GetInWallType();
}