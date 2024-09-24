namespace Models;

public class FireWallType : IWallType
{
    public State Effect => State.Death;
}