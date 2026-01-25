
using System;
using ToyRobot.Enums;

namespace ToyRobot.Commands;

public interface ICommandParser
{
    bool TryParse(string? userCommand, out ParsedCommand? command, out string? error);
}

public class CommandParser: ICommandParser
{
    public bool TryParse(string? userCommand, out ParsedCommand? command, out string? error)
    {
        command = null;
        error = null;

        if (string.IsNullOrWhiteSpace(userCommand))
        {
            error = "Empty command";
            return false;
        }

        var userCmd = userCommand.Trim();

        if (userCmd.Equals("HELP", StringComparison.OrdinalIgnoreCase))
        {
            error = "HELP";
            return false;
        }

        if (userCmd.StartsWith("PLACE", StringComparison.OrdinalIgnoreCase))
        {
            var twoCmds = userCmd.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (twoCmds.Length != 2)
            {
                error = "Invalid PLACE command. Expected: PLACE X,Y,FACING";
                return false;
            }

            var args = twoCmds[1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (args.Length != 3)
            {
                error = "Invalid PLACE command. Expected: PLACE X,Y,FACING";
                return false;
            }

            if (!int.TryParse(args[0], out var x) || !int.TryParse(args[1], out var y))
            {
                error = "Invalid PLACE command. X and Y must be integers";
                return false;
            }

            if (!TryParseFacing(args[2], out var direction))
            {
                error = "Invalid PLACE command. FACING must be NORTH, EAST, SOUTH, or WEST";
                return false;
            }

            command = new ParsedCommand(SimCommandType.Place, x, y, direction);
            return true;
        }

        if (userCmd.Equals("MOVE", StringComparison.OrdinalIgnoreCase)) { command = new ParsedCommand(SimCommandType.Move); return true; }
        if (userCmd.Equals("LEFT", StringComparison.OrdinalIgnoreCase)) { command = new ParsedCommand(SimCommandType.Left); return true; }
        if (userCmd.Equals("RIGHT", StringComparison.OrdinalIgnoreCase)) { command = new ParsedCommand(SimCommandType.Right); return true; }
        if (userCmd.Equals("REPORT", StringComparison.OrdinalIgnoreCase)) { command = new ParsedCommand(SimCommandType.Report); return true; }

        error = "Unknown command. Type 'help' for available commands.";
        return false;
    }

    private static bool TryParseFacing(string input, out Direction facing)
    {
        facing = default;

        switch (input.Trim().ToUpperInvariant())
        {
            case "NORTH": 
                facing = Direction.North; 
                
                return true;
            case "EAST": 
                facing = Direction.East; 
                
                return true;
            case "SOUTH": 
                facing = Direction.South; 
                
                return true;
            case "WEST": 
                facing = Direction.West; 

                return true;
            default: 
                return false;
        }
    }
}
