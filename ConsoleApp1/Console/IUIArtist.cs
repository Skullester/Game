namespace UI;

public interface IUIArtist
{
    void Draw();
}

public class PlayerArtist : IUIArtist
{
    public void Draw()
    {
        throw new NotImplementedException();
    }
}

public class MenuArtist : IUIArtist
{
    public void Draw()
    {
        throw new NotImplementedException();
    }
}