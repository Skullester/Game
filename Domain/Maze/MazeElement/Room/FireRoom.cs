namespace Models.Maze;

public class FireRoom : IRoom
{
    public TimeSpan StayTime => TimeSpan.FromMilliseconds(1000);
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
    public ConsoleColor Color => ConsoleColor.Red;
}