namespace UI;

public class Artist
{
    public static void Draw(IDrawing drawing)
    {
        drawing.Draw();
    }

    public static void DrawMenu()
    {
        Console.ForegroundColor = ConsoleColor.Red;

        int height = 100, width = 100;
        var buffer = new char[height, width];

        Console.SetWindowSize(width, height);
        Console.SetBufferSize(width, height);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var x = (j - width / 2) / 2.5;
                var y = -1 * (i - height / 2);
                var firstPart = x * x;
                var secondPart = (int)((y - Math.Sqrt(Math.Abs(x))) * (y - Math.Abs(x)));

                var heartSize = 500;

                if (firstPart + secondPart <= heartSize)
                {
                    buffer[i, j] = '@';
                }
                else
                {
                    buffer[i, j] = ' ';
                }
            }
        }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(buffer[i, j]);
            }

            Console.WriteLine();
        }

        Console.ReadLine();
    }
}