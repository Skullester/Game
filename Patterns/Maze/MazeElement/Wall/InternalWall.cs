﻿namespace Models;

public class InternalWall : IWall
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
    private float durability;

    public float Durability
    {
        get => durability;
        set
        {
            if (value < 0) throw new ArgumentException("Ошибка");
            durability = value;
        }
    }

    public InternalWall(float durability, IWallType type)
    {
        Durability = durability;
        Type = type;
    }

    public IWallType Type { get; }
    public IWall Clone() => (MemberwiseClone() as IWall)!;
}