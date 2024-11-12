using Models.Maze;
using Models.Naming;

namespace Models.Fabric;

public abstract class MazeFactory : INaming
{
    public abstract IRoom GetRoom();
    public abstract WallType GetWallType();
    public abstract string Name { get; }
}