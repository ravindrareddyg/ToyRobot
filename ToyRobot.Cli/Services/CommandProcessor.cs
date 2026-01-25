using System;
using ToyRobot.Commands;
using ToyRobot.Models;

namespace ToyRobot.Services;

public interface ICommandProcessor
{
    string? ReadLine();
    void WriteStdOutLine(string value);
    void WriteStdErr(string value);
    void WriteStdErrLine(string value);
    void RunInteractive();
}

public class CommandProcessor : ICommandProcessor
{
    private readonly IRobotSimulator _robotSimulator;
    private readonly ICommandParser _commandParser;

    public CommandProcessor(IRobotSimulator robotSimulator, ICommandParser commandParser)
    {
        _robotSimulator = robotSimulator;
        _commandParser = commandParser;
    }

    public string? ReadLine() => Console.ReadLine();
    public void WriteStdOutLine(string value) => Console.Out.WriteLine(value);
    public void WriteStdErr(string value) => Console.Error.Write(value);
    public void WriteStdErrLine(string value) => Console.Error.WriteLine(value);

    public void RunInteractive()
    {
        PrintWelcome();

        while (true)
        {
            WriteStdErr(GetPrompt(_robotSimulator.State));
            var line = ReadLine();
            if (line is null) return;

            var trimmed = line.Trim();

            if (trimmed.Equals("EXIT", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("QUIT", StringComparison.OrdinalIgnoreCase))
                return;

            if (trimmed.Equals("HELP", StringComparison.OrdinalIgnoreCase))
            {
                PrintHelp();
                continue;
            }

            if (!_commandParser.TryParse(trimmed, out var cmd, out var parseError))
            {
                if (parseError == "HELP")
                {
                    PrintHelp();
                    continue;
                }

                WriteStdErrLine(parseError ?? "Invalid command");
                continue;
            }

            var outcome = _robotSimulator.Apply(cmd!);

            if (outcome.IsReport && !string.IsNullOrWhiteSpace(outcome.ReportText))
            {
                WriteStdOutLine(outcome.ReportText!);
                continue;
            }

            if (!outcome.Success && !string.IsNullOrWhiteSpace(outcome.Message))
                WriteStdErrLine(outcome.Message!);
        }
    }

    private void PrintWelcome()
    {
        WriteStdErrLine("Toy Robot Simulator");
        WriteStdErrLine("Type 'help' for commands. Type 'exit' to quit.");
        WriteStdErrLine(string.Empty);
    }

    private void PrintHelp()
    {
        WriteStdErrLine("Commands:");
        WriteStdErrLine("  PLACE X,Y,Facing   (Facing: NORTH|EAST|SOUTH|WEST)");
        WriteStdErrLine("  MOVE");
        WriteStdErrLine("  LEFT");
        WriteStdErrLine("  RIGHT");
        WriteStdErrLine("  REPORT");
        WriteStdErrLine("  help");
        WriteStdErrLine("  exit");
        WriteStdErrLine(string.Empty);
    }

    private static string GetPrompt(RobotState state) =>
    state.IsPlaced ? "toyrobot[placed]> " : "toyrobot[unplaced]> ";
}