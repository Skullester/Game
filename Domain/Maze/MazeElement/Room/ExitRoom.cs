namespace Models.Maze;

public class ExitRoom : IRoom
{
    public TimeSpan StayTime => TimeSpan.MaxValue;
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}