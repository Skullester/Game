﻿namespace Game;

public class Medium : Difficulty
{
    public override string Name => "Средний";

    public Medium() : base(0.5)
    {
    }
}