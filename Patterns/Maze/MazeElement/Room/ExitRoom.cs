namespace Models;

public class ExitRoom : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
}