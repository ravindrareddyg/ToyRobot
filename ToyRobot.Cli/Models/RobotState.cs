using ToyRobot.Enums;

namespace ToyRobot.Models;

public sealed record RobotState(
    bool IsPlaced,
    Position? Position,
    Direction? Facing)
{
    public static RobotState NotPlaced { get; } =
        new(false, null, null);

    public static RobotState Placed(Position position, Direction facing) =>
        new(true, position, facing);

    public RobotState RotateLeft()
    {
        Direction newFacing;

        switch (Facing!.Value)
        {
            case Direction.North: newFacing = Direction.West; break;
            case Direction.West: newFacing = Direction.South; break;
            case Direction.South: newFacing = Direction.East; break;
            case Direction.East: newFacing = Direction.North; break;
            default: newFacing = Facing.Value; break;
        }

        return new RobotState(
            IsPlaced,
            Position,
            Facing: newFacing);
    }

    public RobotState RotateRight()
    {
        Direction newFacing;

        switch (Facing!.Value)
        {
            case Direction.North: newFacing = Direction.East; break;
            case Direction.East: newFacing = Direction.South; break;
            case Direction.South: newFacing = Direction.West; break;
            case Direction.West: newFacing = Direction.North; break;
            default: newFacing = Facing.Value; break;
        }

        return new RobotState(
           IsPlaced,
           Position,
           Facing: newFacing);
    }
}

