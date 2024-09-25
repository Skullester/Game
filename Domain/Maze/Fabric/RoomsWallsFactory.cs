using Application.Naming;

namespace Models.Fabric;

public abstract class MazeFactory : INaming
{
    public abstract IRoom GetRoom();
    public abstract IWallType GetWallType();
    public abstract string Name { get; }
}