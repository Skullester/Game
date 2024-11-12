﻿namespace Models.Maze;

public class ExternalWall : IWall
{
    public WallType Type { get; }
    public ConsoleColor Color => ConsoleColor.White;

    public ExternalWall(WallType type)
    {
        Type = type;
    }

    public IWall Clone() => (MemberwiseClone() as ExternalWall)!;
}