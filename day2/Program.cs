var lines = await File.ReadAllLinesAsync(args[0]);

var reports = lines
    .Select(line => line.Split(' ').Select(int.Parse).ToArray())
    .ToArray();

var safeCount = 0;

foreach (var report in reports)
{
    var deltas = Enumerable.Range(0, report.Length - 1)
        .Select(i => report[i + 1] - report[i])
        .ToArray();

    var isUnsafe = deltas.Any(d => d == 0 || Math.Abs(d) > 3)
        || (deltas.Any(d => d < 0) && deltas.Any(d => d > 0));

    if (!isUnsafe)
    {
        safeCount++;
    }
}

Console.WriteLine("{0} reports are safe.", safeCount);
