namespace Models.Maze;

public class ExitRoom : IRoom, IColorable
{
    public TimeSpan StayTime => TimeSpan.MaxValue;
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
    public ConsoleColor Color => ConsoleColor.Green;
}