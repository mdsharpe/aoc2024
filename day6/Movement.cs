namespace day6;

public static class Movement
{
    public static Coords Move(Coords fromWhere, Direction inDirection)
    {
        switch (inDirection)
        {
            case Direction.Up:
                return fromWhere with { Y = fromWhere.Y - 1 };
            case Direction.Right:
                return fromWhere with { X = fromWhere.X + 1 };
            case Direction.Down:
                return fromWhere with { Y = fromWhere.Y + 1 };
            case Direction.Left:
                return fromWhere with { X = fromWhere.X - 1 };
            default:
                throw new ArgumentOutOfRangeException(nameof(inDirection));
        }
    }

    public static Direction Turn(Direction fromDirection)
    {
        switch (fromDirection)
        {
            case Direction.Up:
                return Direction.Right;
            case Direction.Right:
                return Direction.Down;
            case Direction.Down:
                return Direction.Left;
            case Direction.Left:
                return Direction.Up;
            default:
                throw new ArgumentOutOfRangeException(nameof(fromDirection));
        }
    }
}
