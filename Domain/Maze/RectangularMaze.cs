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
            var neighbours = GetNeighbours(currentPoint);
            ref var element = ref Elements[currentPoint.X, currentPoint.Y];
            var distance = element.Distance;
            element = Room.Clone();
            element.Distance = distance;
            element.IsVisited = true;

            if (neighbours.Count > 0)
            {
                var currentMazeElement = this[currentPoint.X, currentPoint.Y];
                var chosenPoint = neighbours[rand.Next(neighbours.Count)];
                var removedWallPoint = RemoveWall(currentPoint, chosenPoint);
                var removeWallElement = this[removedWallPoint.X, removedWallPoint.Y];
                removeWallElement.IsVisited = true;
                removeWallElement.Distance = currentMazeElement.Distance + 1;
                this[chosenPoint.X, chosenPoint.Y].Distance = currentMazeElement.Distance + 2;
                stack.Push(chosenPoint);
                currentPoint = chosenPoint;
            }
            else
            {
                currentPoint = stack.Pop();
            }
        } while (stack.Count > 0);
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

    /*public List<Point> GetPathList()
    {
        var resultList = new List<Point>();
        var stack = new Stack<Point>();
        var visitedMap = new HashSet<Point>();
        var startPoint = StartPoint;
        stack.Push(startPoint);
        do
        {
            var point = stack.Pop();
            var x = point.X;
            var y = point.Y;
            if (!visitedMap.Add(point))
            {
                resultList.Remove(point);
                continue;
            }

            resultList.Add(point);
            if (point == ExitPoint)
            {
                return resultList;
            }

            var neighbours = new List<Point>();
            if (x > 0 && Elements[x - 1, y] is not IWall) neighbours.Add(new Point(x - 1, y));
            if (x < Height - 1 && Elements[x + 1, y] is not IWall) neighbours.Add(new Point(x + 1, y));
            if (y > 0 && Elements[x, y - 1] is not IWall) neighbours.Add(new Point(x, y - 1));
            if (y < Width - 1 && Elements[x, y + 1] is not IWall) neighbours.Add(new Point(x, y + 1));
            foreach (var neighbour in neighbours)
            {
                if (!visitedMap.Contains(neighbour))
                {
                    stack.Push(neighbour);
                }
            }
        } while (stack.Count > 0);

        return [];
    }
*/

    private List<Point> GetNeighbours(Point point)
    {
        var neighbours = new List<Point>();
        var x = point.X;
        var y = point.Y;
        if (x > 2 && !Elements[x - 2, y].IsVisited) neighbours.Add(new Point(x - 2, y));
        if (y > 2 && !Elements[x, y - 2].IsVisited) neighbours.Add(new Point(x, y - 2));
        if (x < Height - 1 - 2 && !Elements[x + 2, y].IsVisited) neighbours.Add(new Point(x + 2, y));
        if (y < Width - 1 - 2 && !Elements[x, y + 2].IsVisited) neighbours.Add(new Point(x, y + 2));
        return neighbours;
        //ломаем стенку через один
        //P**
        //012
        //Если ломаем 2, 1 автоматом становится комнатой
    }

    public List<Point> GetPathList()
    {
        throw new NotImplementedException();
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