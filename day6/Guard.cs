namespace day6;

public class Guard : Actor
{
    public Guard(Coords location, Direction direction)
    {
        Location = location;
        Direction = direction;
    }

    public Coords Location { get; set; }
    public Direction Direction { get; set; }
}
