using System.Collections;
using Models.Fabric;

namespace Models.Maze;

public class RectangularMaze : IMaze
{
    public int Height { get; set; }
    public int Width { get; set; }
    public Point StartPoint { get; private set; }

    public IRoom Room { get; }

    public WallType WallType { get; }

    public IMazeElement[,] Elements { get; private set; } = null!;

    public Point ExitPoint { get; private set; }

    public RectangularMaze(MazeFactory factory)
    {
        Room = factory.GetRoom();
        WallType = factory.GetWallType();
    }

    public void Generate()
    {
        InitializeWalls();
        InitializeObjects();
    }

    private void FindFurthestExit(int[,] distanceFromStart)
    {
        var furthest = StartPoint;
        for (var x = 1; x < Height; x++)
        {
            if (distanceFromStart[x, Width - 2] > distanceFromStart[furthest.X, furthest.Y])
                furthest = new Point(x, Width - 2);
            if (distanceFromStart[x, 1] > distanceFromStart[furthest.X, furthest.Y])
                furthest = new Point(x, 1);
        }

        for (var y = 1; y < Width; y++)
        {
            if (distanceFromStart[Height - 2, y] > distanceFromStart[furthest.X, furthest.Y])
                furthest = new Point(Height - 2, y);
            if (distanceFromStart[1, y] > distanceFromStart[furthest.X, furthest.Y])
                furthest = new Point(1, y);
        }

        Point point = default;
        //самая дальная точка...
        if (furthest.X == 1) point = new Point(furthest.X - 1, furthest.Y); //в первой строке - ломаем вверх
        else if (furthest.Y == 1) point = new Point(furthest.X, furthest.Y - 1); //в первом столбце - ломаем влево
        else if (furthest.X == Height - 2)
            point = new Point(furthest.X + 1, furthest.Y); //в последней строке - ломаем вниз
        else if (furthest.Y == Width - 2)
            point = new Point(furthest.X, furthest.Y + 1); //в последнем столбце - ломаем вправо
        this[point.X, point.Y] = new ExitRoom();
        ExitPoint = point;
    }

    private void InitializeWalls()
    {
        Elements = new IMazeElement[Height, Width];
        var exWall = new ExternalWall(WallType);
        var inWall = new InternalWall(WallType);
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                if (i == 0 || i == Height - 1 || j == 0 || j == Width - 1)
                    this[i, j] = exWall.Clone();
                else this[i, j] = inWall.Clone();
            }
        }
    }

    private void InitializeObjects()
    {
        var rand = new Random();
        var isVisited = new bool[Height, Width];
        var distanceFromStart = new int[Height, Width];
        Point[] startPositions =
        [
            new Point(1, 1),
            new Point(Height - 2, 1),
            new Point(1, Width - 2),
            new Point(Height - 2, Width - 2)
        ];
        StartPoint = startPositions[rand.Next(startPositions.Length)];
        var currentPoint = StartPoint;
        var stack = new Stack<Point>();
        do
        {
            var neighbours = GetNeighbours(currentPoint, isVisited);
            Elements[currentPoint.X, currentPoint.Y] = Room.Clone();
            isVisited[currentPoint.X, currentPoint.Y] = true;
            if (neighbours.Count > 0)
            {
                var chosenPoint = neighbours[rand.Next(neighbours.Count)];
                var wallPoint = RemoveWall(currentPoint, chosenPoint);
                isVisited[wallPoint.X, wallPoint.Y] = true;
                var currentDistance = distanceFromStart[currentPoint.X, currentPoint.Y];
                distanceFromStart[wallPoint.X, wallPoint.Y] = currentDistance + 1;
                distanceFromStart[chosenPoint.X, chosenPoint.Y] = currentDistance + 2;
                stack.Push(chosenPoint);
                currentPoint = chosenPoint;
            }
            else
            {
                currentPoint = stack.Pop();
            }
        } while (stack.Count > 0);

        FindFurthestExit(distanceFromStart);
    }

    private Point RemoveWall(Point currentPoint, Point breakableWallPoint)
    {
        Point point;
        if (currentPoint.X == breakableWallPoint.X)
            point = currentPoint.Y < breakableWallPoint.Y
                ? new Point(currentPoint.X, currentPoint.Y + 1)
                : new Point(currentPoint.X, currentPoint.Y - 1);
        else
            point = currentPoint.X < breakableWallPoint.X
                ? new Point(currentPoint.X + 1, currentPoint.Y)
                : new Point(currentPoint.X - 1, currentPoint.Y);
        this[point.X, point.Y] = Room.Clone();
        return point;
    }
    private List<Point> GetNeighbours(Point point, bool[,] isVisited)
    {
        var neighbours = new List<Point>();
        var x = point.X;
        var y = point.Y;
        if (x > 2 && !isVisited[x - 2, y]) neighbours.Add(new Point(x - 2, y));
        if (y > 2 && !isVisited[x, y - 2]) neighbours.Add(new Point(x, y - 2));
        if (x < Height - 1 - 2 && !isVisited[x + 2, y]) neighbours.Add(new Point(x + 2, y));
        if (y < Width - 1 - 2 && !isVisited[x, y + 2]) neighbours.Add(new Point(x, y + 2));
        return neighbours;
        //ломаем стенку через один
        //P**
        //012
        //Если ломаем 2, 1 автоматом становится комнатой
    }

    public IMazeElement this[int x, int y]
    {
        get => Elements[x, y];
        set => Elements[x, y] = value;
    }

    public IEnumerator<IMazeElement> GetEnumerator()
    {
        return Elements.Cast<IMazeElement>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}