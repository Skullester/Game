namespace Models.Maze;

public class Room : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
    public TimeSpan StayTime => TimeSpan.MaxValue;
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}