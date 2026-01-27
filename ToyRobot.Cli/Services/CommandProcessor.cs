using System;
using ToyRobot.Commands;

namespace ToyRobot.Services;

public interface ICommandProcessor
{
    void Run();
}

public sealed class CommandProcessor : ICommandProcessor
{
    private readonly IRobotSimulator _robotSimulator;
    private readonly ICommandParser _commandParser;

    public CommandProcessor(
        IRobotSimulator robotSimulator,
        ICommandParser commandParser)
    {
        _robotSimulator = robotSimulator;
        _commandParser = commandParser;
    }

    public void Run()
    {
        var isRedirected = Console.IsInputRedirected;

        if (isRedirected)
        {
            RunFromStdIn();
            return;
        }

        RunInteractive();
    }

    private void RunFromStdIn()
    {
        string? line;
        while ((line = Console.ReadLine()) != null)
        {
            ProcessLine(line);
        }
    }

    private void RunInteractive()
    {
        string? line;
        while ((line = Console.ReadLine()) != null)
        {
            ProcessLine(line);
        }
    }

    private void ProcessLine(string line)
    {
        if (!_commandParser.TryParse(line, out var command) || command is null)
            return;
        var outcome = _robotSimulator.Apply(command);

        if (outcome.IsReport && !string.IsNullOrWhiteSpace(outcome.ReportText))
        {
            Console.Out.WriteLine(outcome.ReportText);
        }
    }
}
