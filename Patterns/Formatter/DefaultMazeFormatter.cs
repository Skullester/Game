using Models;

namespace UI;

public class DefaultMazeFormatter : IMazeFormatter
{
    public string Name => "Стандартный";
    public IReadOnlyList<char> Symbols => symbolsArr.AsReadOnly();
    private readonly char[] symbolsArr = [' ', '#', '*', 'P'];

    public char Format(IMazeElement element)
    {
        return element switch
        {
            Room or ExitRoom => ' ',
            ExternalWall => '#',
            InternalWall => '*',
            Player => 'P',
            _ => '?',
        };
    }
}