namespace Models;

public class FireRoom : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
    public bool Dangerous { get; set; }
    public int Time => 1000;
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}