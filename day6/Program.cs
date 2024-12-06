using day6;

var map = await File.ReadAllLinesAsync(args[0]);
var width = map.Select(o => o.Length).Max();
var height = map.Length;

var actors = new List<Actor>();
Guard guard = null!;
Dictionary<Coords, HashSet<Direction>> guardVisitedLocations = [];
List<Coords> loopObstructionsLocationsToTry = [];

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var here = new Coords(x, y);
        loopObstructionsLocationsToTry.Add(here);
        guardVisitedLocations.Add(here, []);

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

var guardStartingLocation = guard.Location;
var guardStartingDirection = guard.Direction;

void Run()
{
    do
    {
        if (guardVisitedLocations[guard.Location].Contains(guard.Direction))
        {
            throw new LoopException();
        }

        guardVisitedLocations[guard.Location].Add(guard.Direction);

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

                if (directionsTried.Contains(guard.Direction))
                {
                    throw new Exception("Guard is stuck.");
                }
            }
        } while (targetLocationOccupied);

        guard.Location = targetLocation;
    } while (guard.Location.X >= 0 && guard.Location.X < width
        && guard.Location.Y >= 0 && guard.Location.Y < height);
}

Run();
Console.WriteLine("Guard visits {0} distinct locations.", guardVisitedLocations.Where(o => o.Value.Any()).Count());

HashSet<Coords> loopObstructionLocations = [];
var loopObstruction = new LoopObstruction();
for (var i = 0; i < loopObstructionsLocationsToTry.Count; i++)
{
    var loopObstructionLocation = loopObstructionsLocationsToTry[i];

    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write("{0} of {1}", i, loopObstructionsLocationsToTry.Count);

    guard.Location = guardStartingLocation;
    guard.Direction = guardStartingDirection;
    if (actors.Any(o => o.Location == loopObstructionLocation))
    {
        continue;
    }

    loopObstruction.Location = loopObstructionLocation;
    actors.Add(loopObstruction);

    foreach (var guardVisitedLocation in guardVisitedLocations)
    {
        guardVisitedLocation.Value.Clear();
    }

    try
    {
        Run();
    }
    catch (LoopException)
    {
        loopObstructionLocations.Add(loopObstructionLocation);
    }

    actors.Remove(loopObstruction);
}

Console.WriteLine();
Console.WriteLine("Found {0} positions where an obstruction would cause a loop.", loopObstructionLocations.Count);
