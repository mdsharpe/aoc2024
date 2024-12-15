namespace day9;

public class Defraggler
{
    public static void Run(IEnumerable<int?> blocks)
    {
        var disk = ParseDisk(blocks).ToList();

        bool TryMoveFile(File file)
        {
            var x = disk.IndexOf(file);

            var gap = disk
                .Take(x - 1)
                .OfType<EmptySpace>()
                .Where(o => o.Length >= file.Length)
                .FirstOrDefault();

            if (gap is null)
            {
                return false;
            }

            var i = disk.IndexOf(file);
            disk.Remove(file);
            disk.Insert(i, new EmptySpace(file.Length));
            i = disk.IndexOf(gap);
            disk.Insert(i, file);
            gap.Length -= file.Length;

            return true;
        }

        void ConsolidateSpace()
        {
            int len;

            do
            {
                len = disk.Count;

                for (var i = 0; i < len - 2; i++)
                {
                    if (disk[i] is EmptySpace toMerge
                        && disk[i + 1] is EmptySpace toRemove)
                    {
                        toMerge.Length += toRemove.Length;
                        disk.Remove(toRemove);
                    }
                }
            } while (disk.Count < len);
        }

        foreach (var file in disk.Reverse<IDiskEntry>().OfType<File>().ToArray())
        {
            if (TryMoveFile(file))
            {
                ConsolidateSpace();
            }
        }

        blocks = disk.SelectMany(o => o.ToBlockArray());

        Console.WriteLine("Checksum defragged: {0}", Checksum.Compute(blocks));
    }

    private static IEnumerable<IDiskEntry> ParseDisk(IEnumerable<int?> blocks)
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

        foreach (var block in blocks)
        {
            if (block != fileId)
            {
                yield return MakeDiskEntry();
                fileId = block;
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
        IEnumerable<int?> ToBlockArray();
    }

    private class File(int id, int length) : IDiskEntry
    {
        public int Id { get; } = id;
        public int Length { get; } = length;

        public override string ToString()
            => Enumerable.Repeat(Id.ToString(), Length)
                .Aggregate(string.Concat);

        public IEnumerable<int?> ToBlockArray()
            => Enumerable.Repeat<int?>(Id, Length);
    }

    private class EmptySpace(int length) : IDiskEntry
    {
        public int Length { get; set; } = length;

        public override string ToString()
            => new('.', Length);

        public IEnumerable<int?> ToBlockArray()
            => Enumerable.Repeat<int?>(null, Length);
    }
}
