using day9;

var input = (await File.ReadAllTextAsync(args[0])).Trim();

var disk = new List<int?>();

for (var i = 0; i < input.Length; i++)
{
    var c = input[i];
    var n = int.Parse(c.ToString());

    int? fileIndex = i % 2 == 0 ? i / 2 : null;
    disk.AddRange(Enumerable.Repeat(fileIndex, n));
}

Condenser.Run(disk.ToList());
Defraggler.Run(disk.ToList());
