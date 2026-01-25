using System;

namespace ToyRobot.Models;

public record Tabletop(int Width, int Height)
{
    public int Width { get; init; } =
        Width > 0 ? Width : throw new ArgumentOutOfRangeException(nameof(Width), "Width must be > 0");
    public int Height { get; init; } = 
        Height > 0 ? Height : throw new ArgumentOutOfRangeException(nameof(Height), "Height must be > 0");

    public bool IsValid(Position p) =>
        p.X >= 0 && p.X < Width &&
        p.Y >= 0 && p.Y < Height;
}