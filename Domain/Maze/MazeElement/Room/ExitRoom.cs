namespace Models;

public class ExitRoom : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
    public int StayTime => int.MaxValue;
    public IRoom Clone() => (MemberwiseClone() as IRoom)!;
}