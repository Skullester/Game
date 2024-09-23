using UI;

namespace Game;

class WriterToDrawingAdapter : IDrawing
{
    private readonly MazeWriter writer;

    public WriterToDrawingAdapter(MazeWriter writer)
    {
        this.writer = writer;
    }

    public void Draw()
    {
        writer.Write();
    }
}