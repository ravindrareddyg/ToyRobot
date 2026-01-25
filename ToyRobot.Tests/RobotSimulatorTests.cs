using ToyRobot.Commands;
using ToyRobot.Enums;
using ToyRobot.Models;
using ToyRobot.Services;
using Xunit;

namespace ToyRobot.Tests;

public class RobotSimulatorTests
{
    private static IRobotSimulator CreateSim()
    {
        var table = new Tabletop(5, 5);
        var handler = new RobotCommandHandler();
        return new RobotSimulator(table, handler);
    }

    [Fact]
    public void Move_BeforePlace_IsRejected()
    {
        var sim = CreateSim();

        var outcome = sim.Apply(new ParsedCommand(SimCommandType.Move));

        Assert.False(outcome.Success);
        Assert.Contains("not been placed", outcome.Message);
    }

    [Fact]
    public void Place_OutOfBounds_IsRejected()
    {
        var sim = CreateSim();

        var outcome = sim.Apply(new ParsedCommand(SimCommandType.Place, X: 5, Y: 0, Facing: Direction.North));

        Assert.False(outcome.Success);
        Assert.Contains("Cannot PLACE", outcome.Message);
    }

    [Fact]
    public void Place_ThenMove_UpdatesState()
    {
        var sim = CreateSim();

        Assert.True(sim.Apply(new ParsedCommand(SimCommandType.Place, 0, 0, Direction.North)).Success);
        Assert.True(sim.Apply(new ParsedCommand(SimCommandType.Move)).Success);

        var report = sim.Apply(new ParsedCommand(SimCommandType.Report));

        Assert.True(report.IsReport);
        Assert.Equal("0,1,NORTH", report.ReportText);
    }

    [Fact]
    public void Move_ThatWouldFall_IsRejected()
    {
        var sim = CreateSim();

        Assert.True(sim.Apply(new ParsedCommand(SimCommandType.Place, 0, 4, Direction.North)).Success);

        var outcome = sim.Apply(new ParsedCommand(SimCommandType.Move));

        Assert.False(outcome.Success);
        Assert.Contains("fall off", outcome.Message);
    }

    [Fact]
    public void Left_Right_RotateCorrectly()
    {
        var sim = CreateSim();

        Assert.True(sim.Apply(new ParsedCommand(SimCommandType.Place, 0, 0, Direction.North)).Success);
        Assert.True(sim.Apply(new ParsedCommand(SimCommandType.Left)).Success);

        var report1 = sim.Apply(new ParsedCommand(SimCommandType.Report));
        Assert.Equal("0,0,WEST", report1.ReportText);

        Assert.True(sim.Apply(new ParsedCommand(SimCommandType.Right)).Success);

        var report2 = sim.Apply(new ParsedCommand(SimCommandType.Report));
        Assert.Equal("0,0,NORTH", report2.ReportText);
    }
}
