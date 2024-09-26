using System.Collections;
using System.Drawing;
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

    public RectangularMaze(MazeFactory factory)
    {
        Room = factory.GetRoom();
        WallType = factory.GetWallType();
    }

    public void Generate()
    {
        InitializeExternalWalls();
        InitializeObjects();
        FindFurthestExit();
    }

    private void FindFurthestExit()
    {
        var furthest = StartPoint;
        for (var x = 1; x < Height; x++)
        {
            if (Elements[x, Width - 2].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(x, Width - 2);
            if (Elements[x, 1].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(x, 1);
        }

        for (var y = 1; y < Width; y++)
        {
            if (Elements[Height - 2, y].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(Height - 2, y);
            if (Elements[1, y].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(1, y);
        }

        Point point = default;
        if (furthest.X == 1) point = new Point(furthest.X - 1, furthest.Y);
        else if (furthest.Y == 1) point = new Point(furthest.X, furthest.Y - 1);
        else if (furthest.X == Height - 2) point = new Point(furthest.X + 1, furthest.Y);
        else if (furthest.Y == Width - 2) point = new Point(furthest.X, furthest.Y + 1);
        this[point.X, point.Y] = new ExitRoom();
    }

    private void InitializeExternalWalls()
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
            var neighbours = new List<Point>();
            var x = currentPoint.X;
            var y = currentPoint.Y;
            var distance = this[x, y].Distance;
            this[x, y] = Room.Clone();
            this[x, y].Distance = distance;
            this[x, y].IsVisited = true;
            if (x > 2 && !Elements[x - 2, y].IsVisited) neighbours.Add(new Point(x - 2, y));
            if (y > 2 && !Elements[x, y - 2].IsVisited) neighbours.Add(new Point(x, y - 2));
            if (x < Height - 1 - 2 && !Elements[x + 2, y].IsVisited) neighbours.Add(new Point(x + 2, y));
            if (y < Width - 1 - 2 && !Elements[x, y + 2].IsVisited) neighbours.Add(new Point(x, y + 2));

            if (neighbours.Count > 0)
            {
                var chosenPoint = neighbours[rand.Next(neighbours.Count)];
                RemoveWall(currentPoint, chosenPoint);
                this[chosenPoint.X, chosenPoint.Y].Distance = this[currentPoint.X, currentPoint.Y].Distance + 1;
                stack.Push(chosenPoint);
                currentPoint = chosenPoint;
            }
            else currentPoint = stack.Pop();
        } while (stack.Count > 0);
    }

    private Point RemoveWall(Point a, Point b)
    {
        Point point;
        if (a.X == b.X)
            point = a.Y < b.Y ? new Point(a.X, a.Y + 1) : new Point(a.X, a.Y - 1);
        else
            point = a.X < b.X ? new Point(a.X + 1, a.Y) : new Point(a.X - 1, a.Y);

        this[point.X, point.Y] = Room.Clone();
        this[point.X, point.Y].IsVisited = true;
        return point;
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