namespace day9;

public static class Checksum
{
    public static long Compute(IEnumerable<int?> blocks)
        => blocks
            .Select((n, i) => Convert.ToInt64((n ?? 0) * i))
            .Sum();
}
