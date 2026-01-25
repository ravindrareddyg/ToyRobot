using ToyRobot.Enums;
using ToyRobot.Models;

namespace ToyRobot.Commands;

public enum SimCommandType
{
    Place,
    Move,
    Left,
    Right,
    Report
}

public record ParsedCommand(
    SimCommandType Type,
    int? X = null,
    int? Y = null,
    Direction? Facing = null);

public record CommandOutcome(
    bool Success,
    string? Message = null,
    bool IsReport = false,
    string? ReportText = null)
{
    public static CommandOutcome Ok() => new(true);
    public static CommandOutcome Error(string msg) => new(false, msg);
    public static CommandOutcome Report(string text) => new(true, IsReport: true, ReportText: text);
}

public record CommandResult(RobotState State, CommandOutcome Outcome);
