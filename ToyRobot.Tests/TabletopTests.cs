using System;
using ToyRobot.Models;
using Xunit;

namespace ToyRobot.Tests;

public sealed class TabletopTests
{
    [Fact]
    public void IsValid_ReturnsTrue_ForPositionInsideTable()
    {
        Position position = new Position(2, 3);
        Tabletop tableTop = new Tabletop(5, 5);

        bool result = tableTop.IsValid(position);

        Assert.True(result);
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForPositionOutsideTable()
    {
        Position position = new Position(-1, 0);
        Tabletop tableTop = new Tabletop(5, 5);

        bool result = tableTop.IsValid(position);

        Assert.False(result);
    }

    [Fact]
    public void Constructor_ThrowsArgumentOutOfRangeException_ForInvalidDimensions()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tabletop(0, 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tabletop(5, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tabletop(-1, 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tabletop(5, -1));
    }
}