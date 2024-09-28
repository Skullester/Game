namespace Models.Maze;

public class Room : IRoom
{
    public TimeSpan StayTime => TimeSpan.MaxValue;
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}