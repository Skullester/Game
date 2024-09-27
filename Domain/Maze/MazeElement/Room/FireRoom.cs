namespace Models.Maze;

public class FireRoom : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
    public TimeSpan StayTime => TimeSpan.FromMilliseconds(1000);
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}