using day6;

var map = await File.ReadAllLinesAsync(args[0]);
var width = map.Select(o => o.Length).Max();
var height = map.Length;

var actors = new List<Actor>();
Guard guard = null!;
HashSet<Coords> guardVisitedLocations = [];

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var here = new Coords(x, y);

        switch (map[y][x])
        {
            case '#':
                actors.Add(new Obstacle(here));
                break;

            case '^':
                guard = new Guard(here, Direction.Up);
                actors.Add(guard);
                break;

            case '.':
            default:
                break;
        }
    }
}

Console.WriteLine("Guard at:\t{0}", guard.Location);

do
{
    guardVisitedLocations.Add(guard.Location);

    HashSet<Direction> directionsTried = [];
    Coords targetLocation;
    bool targetLocationOccupied;

    do
    {
        targetLocation = Movement.Move(guard.Location, guard.Direction);
        directionsTried.Add(guard.Direction);
        targetLocationOccupied = actors.Any(o => o.Location == targetLocation);

        if (targetLocationOccupied)
        {
            guard.Direction = Movement.Turn(guard.Direction);
            Console.WriteLine("Location {0} is occupied; turned {1}", targetLocation, guard.Direction);

            if (directionsTried.Contains(guard.Direction))
            {
                throw new Exception("Guard is stuck.");
            }
        }
    } while (targetLocationOccupied);

    guard.Location = targetLocation;
    Console.WriteLine("Moved to:\t{0}", guard.Location);
} while (guard.Location.X >= 0 && guard.Location.X < width
    && guard.Location.Y >= 0 && guard.Location.Y < height);

Console.WriteLine("Guard visited {0} distinct locations.", guardVisitedLocations.Count);