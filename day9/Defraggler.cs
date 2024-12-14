using System;
using System.Security;

namespace day9;

public class Defraggler
{
    public static void Run(List<int?> blocks)
    {
        var disk = ParseDisk(blocks).ToList();

        bool TryMoveFile()
        {
            for (var x = disk.Count; x > 0; x--)
            {
                if (disk[x] is File file)
                {
                    var gap = disk
                        .Take(x - 1)
                        .OfType<EmptySpace>()
                        .Where(o => o.Length >= file.Length)
                        .FirstOrDefault();

                    if (gap is not null)
                    {
                        disk.Remove(file);
                        var i = disk.IndexOf(gap);
                        disk.Insert(i, file);
                        gap.Length -= file.Length;
                    }
                }
            }
        }

        void ConsolidateSpace()
        {
        }

        while (TryMoveFile())
        {
            ConsolidateSpace();
        }

        var checksum = blocks
            .Where(o => o is not null)
            .Select((n, i) => Convert.ToInt64(n!.Value * i))
            .Sum();

        Console.WriteLine("Checksum defragged: {0}", checksum);
    }

    private static IEnumerable<IDiskEntry> ParseDisk(List<int?> blocks)
    {
        int? fileId = null;
        var len = 0;

        IDiskEntry MakeDiskEntry()
        {
            if (fileId is null)
            {
                return new EmptySpace(len);
            }

            return new File(fileId.Value, len);
        }

        for (var i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] != fileId)
            {
                yield return MakeDiskEntry();
                fileId = blocks[i];
                len = 1;
            }
            else
            {
                len++;
            }
        }

        if (len > 0)
        {
            yield return MakeDiskEntry();
        }
    }

    private interface IDiskEntry
    {
        int Length { get; }
    }

    private class File : IDiskEntry
    {
        public File(int id, int length)
        {
            Id = id;
            Length = length;
        }

        public int Id { get; }
        public int Length { get; }
    }

    private class EmptySpace : IDiskEntry
    {
        public EmptySpace(int length)
        {
            Length = length;
        }

        public int Length { get; set; }
    }
}
