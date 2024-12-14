using System;

namespace day9;

public static class Condenser
{
    public static void Run(List<int?> disk)
    {
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

        while (!GetIsContiguous())
        {
            Condense();
        }

        var checksum = disk
            .Where(o => o is not null)
            .Select((n, i) => Convert.ToInt64(n!.Value * i))
            .Sum();
    
        Console.WriteLine("Checksum condensed: {0}", checksum);
    }
}
