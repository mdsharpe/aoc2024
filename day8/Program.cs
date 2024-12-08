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
List<Antinode> resonantHarmonicNodes = [];

foreach (var frequencyGroup in antennaeByFreq)
{
    foreach (var antenna in frequencyGroup.Value)
    {
        foreach (var otherAntenna in frequencyGroup.Value.Except([antenna]))
        {
            var dx = otherAntenna.Coords.X - antenna.Coords.X;
            var dy = otherAntenna.Coords.Y - antenna.Coords.Y;
            var m = 0;
            bool an1OutOfBounds = false, an2OutOfBounds = false;

            do
            {
                m++;

                bool DoAntinode(Coords antennaCoords, int multiplier)
                {
                    var an = new Antinode(
                        frequencyGroup.Key,
                        new Coords(otherAntenna.Coords.X + (dx * m), otherAntenna.Coords.Y + (dy * m)));

                    if (an.Coords.X < 0 || an.Coords.X >= width
                        || an.Coords.Y < 0 || an.Coords.Y >= height)
                    {
                        return false;
                    }

                    if (m == 1)
                    {
                        antinodes.Add(an);
                    }
                    else
                    {
                        resonantHarmonicNodes.Add(an);
                    }

                    return true;
                }

                an1OutOfBounds = !DoAntinode(otherAntenna.Coords, m);
                an2OutOfBounds = !DoAntinode(antenna.Coords, m * -1);
            } while (!(an1OutOfBounds && an2OutOfBounds));
        }
    }
}

var antinodeLocations = antinodes
    .Select(o => o.Coords)
    .ToHashSet();
var allHarmonicLocations = resonantHarmonicNodes
    .Select(o => o.Coords)
    .Concat(antinodeLocations)
    .Concat(antennaeLocations)
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

        var isHarmonic = allHarmonicLocations.Contains(here);
        if (isHarmonic)
        {
            Console.Write("*");
            continue;
        }

        Console.Write('.');
    }
    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine(
    "{0} antinode locations, {1} including harmonics.",
    antinodeLocations.Count,
    allHarmonicLocations.Count);
