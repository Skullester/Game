namespace Models;

public class FireRoom : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}