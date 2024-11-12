using Models.Maze;

namespace Models.Fabric;

public class IceRoom : IRoom
{
    public TimeSpan StayTime => TimeSpan.MaxValue;
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}