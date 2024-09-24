namespace Models;

public class Room : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }

    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}