using System.Runtime.CompilerServices;
using day8;

var map = await File.ReadAllLinesAsync(args[0]);
var width = map.Select(o => o.Length).Max();
var height = map.Length;

Dictionary<char, List<Antenna>> antennaeByFreq = [];

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var c = map[y][x];
        if (c == '.')
        {
            continue;
        }

        if (!antennaeByFreq.ContainsKey(c))
        {
            antennaeByFreq.Add(c, []);
        }

        antennaeByFreq[c].Add(new Antenna(c, new Coords(x, y)));
    }
}

var antennaeLocations = antennaeByFreq
    .SelectMany(o => o.Value.Select(x => x.Coords))
    .Distinct()
    .ToHashSet();

List<Antinode> antinodes = [];

foreach (var frequencyGroup in antennaeByFreq)
{
    foreach (var antenna in frequencyGroup.Value)
    {
        foreach (var otherAntenna in frequencyGroup.Value.Except([antenna]))
        {
            var dx = otherAntenna.Coords.X - antenna.Coords.X;
            var dy = otherAntenna.Coords.Y - antenna.Coords.Y;

            antinodes.Add(new Antinode(frequencyGroup.Key, new Coords(otherAntenna.Coords.X + dx, otherAntenna.Coords.Y + dy)));
            antinodes.Add(new Antinode(frequencyGroup.Key, new Coords(antenna.Coords.X - dx, antenna.Coords.Y - dy)));
        }
    }
}

var antinodeLocations = antinodes
    .Select(o => o.Coords)
    .Where(o => o.X >= 0 && o.X < width)
    .Where(o => o.Y >= 0 && o.Y < height)
    .ToHashSet();

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var here = new Coords(x, y);

        var antenna = antennaeByFreq
            .SelectMany(o => o.Value)
            .FirstOrDefault(o => o.Coords == here);

        if (antenna is not null)
        {
            Console.Write(antenna.Frequency);
            continue;
        }

        var isAntinode = antinodeLocations.Contains(here);
        if (isAntinode)
        {
            Console.Write('#');
            continue;
        }

        Console.Write('.');
    }
    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine("{0} antinode locations.", antinodeLocations.Count);
