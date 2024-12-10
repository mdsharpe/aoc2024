var input = (await File.ReadAllTextAsync(args[0])).Trim();

var disk = new List<int?>();

bool GetIsContiguous()
{
    var lastOccupied = disk.FindLastIndex(o => o is not null);
    var firstFree = disk.FindIndex(o => o is null);
    return lastOccupied < firstFree;
}

void Condense()
{
    var lastOccupied = disk.FindLastIndex(o => o is not null);
    var firstFree = disk.FindIndex(o => o is null);

    disk[firstFree] = disk[lastOccupied];
    disk[lastOccupied] = null;
}

long GetChecksum() => disk
    .Where(o => o is not null)
    .Select((n, i) => Convert.ToInt64(n!.Value * i))
    .Sum();

for (var i = 0; i < input.Length; i++)
{
    var c = input[i];
    var n = int.Parse(c.ToString());

    int? fileIndex = i % 2 == 0 ? i / 2 : null;
    disk.AddRange(Enumerable.Repeat(fileIndex, n));
}


while (!GetIsContiguous())
{
    Condense();
}

Console.WriteLine("Checksum: {0}", GetChecksum());
