using ToyRobot.Commands;
using ToyRobot.Models;

namespace ToyRobot.Services;

public interface IRobotSimulator
{
    RobotState State { get; }
    CommandOutcome Apply(ParsedCommand command);
}


public class RobotSimulator : IRobotSimulator
{
    private readonly Tabletop _tabletop;
    private readonly IRobotCommandHandler _robotCommandHandler;

    public RobotState State { get; private set; } = RobotState.NotPlaced;

    public RobotSimulator(Tabletop tabletop, IRobotCommandHandler robotCommandHandler)
    {
        _tabletop = tabletop;
        _robotCommandHandler = robotCommandHandler;
    }

    public CommandOutcome Apply(ParsedCommand command)
    {
        var result = _robotCommandHandler.Handle(State, _tabletop, command);
        State = result.State;
        return result.Outcome;
    }
}
