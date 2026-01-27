using ToyRobot.Enums;
using ToyRobot.Models;

namespace ToyRobot.Commands;

public interface IRobotCommandHandler
{
    CommandResult Handle(RobotState state, Tabletop table, ParsedCommand command);
}


public class RobotCommandHandler : IRobotCommandHandler
{
    public CommandResult Handle(RobotState state, Tabletop table, ParsedCommand command)
    {
        switch (command.Type)
        {
            case SimCommandType.Place:
                return Place(state, table, command);
            case SimCommandType.Move:
                return Move(state, table);
            case SimCommandType.Left:
                return TurnLeft(state);
            case SimCommandType.Right:
                return TurnRight(state);
            case SimCommandType.Report:
                return Report(state);
            default:
                return new CommandResult(state, CommandOutcome.Ok());
        }
    }

    private static CommandResult Place(RobotState state, Tabletop table, ParsedCommand cmd)
    {
        if (cmd.X is null || cmd.Y is null || cmd.Facing is null)
            return new CommandResult(state, CommandOutcome.Ok());

        var pos = new Position(cmd.X.Value, cmd.Y.Value);

        if (!table.IsValid(pos))
            return new CommandResult(state, CommandOutcome.Ok());

        return new CommandResult(RobotState.Placed(pos, cmd.Facing.Value), CommandOutcome.Ok());
    }

    private static CommandResult Move(RobotState state, Tabletop table)
    {
        if (!state.IsPlaced || state.Position is null || state.Facing is null)
            return new CommandResult(
                state, 
                CommandOutcome.Ok());

        var next = NextPosition(state.Position.Value, state.Facing.Value);

        if (!table.IsValid(next))
            return new CommandResult(
                state, 
                CommandOutcome.Ok());

        return new CommandResult(state with { Position = next }, CommandOutcome.Ok());
    }

    private static CommandResult TurnLeft(RobotState state)
    {
        if (!state.IsPlaced || state.Facing is null)
            return new CommandResult(
                state,
                CommandOutcome.Ok());

        return new CommandResult(state.RotateLeft(), CommandOutcome.Ok());
    }

    private static CommandResult TurnRight(RobotState state)
    {
        if (!state.IsPlaced || state.Facing is null)
            return new CommandResult(
                state,
                CommandOutcome.Ok());

        return new CommandResult(state.RotateRight(), CommandOutcome.Ok());
    }

    private static CommandResult Report(RobotState state)
    {
        if (!state.IsPlaced || state.Position is null || state.Facing is null)
            return new CommandResult(
                state, 
                CommandOutcome.Ok());

        var p = state.Position.Value;
        var Direction = state.Facing.Value.ToString().ToUpperInvariant();

        return new CommandResult(
            state,
            CommandOutcome.Report($"{p.X},{p.Y},{Direction}"));
    }

    private static Position NextPosition(Position pos, Direction facing)
    {
        switch (facing) {
            case Direction.North:
               return pos with { Y = pos.Y + 1 };
            case Direction.South:
                return pos with { Y = pos.Y - 1 };
            case Direction.East:
                return pos with { X = pos.X + 1 };
            case Direction.West:
                return pos with { X = pos.X - 1 };
            default:
                return pos;
        }
    }
}