var lines = await File.ReadAllLinesAsync(args[0]);

var reports = lines
    .Select(line => line.Split(' ').Select(int.Parse).ToArray())
    .ToArray();

var safeCount = 0;
var safeCountWithDampener = 0;

bool getIsSafe(int[] report)
{
    var deltas = Enumerable.Range(0, report.Length - 1)
        .Select(i => report[i + 1] - report[i])
        .ToArray();

    var isUnsafe = deltas.Any(d => d == 0 || Math.Abs(d) > 3)
        || (deltas.Any(d => d < 0) && deltas.Any(d => d > 0));

    return !isUnsafe;
}

foreach (var report in reports)
{
    var isSafe = getIsSafe(report);

    if (isSafe)
    {
        safeCount++;
        safeCountWithDampener++;
    }
    else
    {
        for (var i = 0; i < report.Length; i++)
        {
            var dampenedReport = Enumerable.Concat(
                report.Take(i),
                report.Skip(i + 1).Take(report.Length - (i + 1)))
                .ToArray();

            isSafe = getIsSafe(dampenedReport);

            if (isSafe)
            {
                safeCountWithDampener++;
                break;
            }
        }
    }
}

Console.WriteLine("{0} reports are safe.", safeCount);
Console.WriteLine("{0} reports are safe with Problem Dampener.", safeCountWithDampener);
