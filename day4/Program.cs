var grid = (await File.ReadAllLinesAsync(args[0]))
    .Select(o => o.ToCharArray())
    .ToArray();

var width = grid.Select(o => o.Length).Max();
var height = grid.Length;

(int ox, int oy, int dx, int dy)[][] searchesSeqsNormal = [
    [(0, 0, 1, 0)],
    [(0, 0, 1, 1)],
    [(0, 0, 0, 1)],
    [(0, 0, -1, 1)],
    [(0, 0, -1, 0)],
    [(0, 0, -1, -1)],
    [(0, 0, 0, -1)],
    [(0, 0, 1, -1)]
];

(int ox, int oy, int dx, int dy)[][] searchSeqsX = [
    [(-1, -1, 1, 1), (1, -1, -1, 1)],
    [(-1, -1, 1, 1), (-1, 1, 1, -1)],
    [(1, 1, -1, -1), (1, -1, -1, 1)],
    [(1, 1, -1, -1), (-1, 1, 1, -1)],
];

IEnumerable<(int x, int y)> FindInstancesOf(string word, bool xMode)
{
    var targetChars = word.ToCharArray();

    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            foreach (var searchSeq in xMode ? searchSeqsX : searchesSeqsNormal)
            {
                var seqSatisfied = true;

                foreach (var dir in searchSeq)
                {
                    int sx = x + dir.ox,
                        sy = y + dir.oy,
                        si = 0;
                    var found = true;

                    do
                    {
                        if (sx < 0 || sx >= width
                        || sy < 0 || sy >= height)
                        {
                            found = false;
                        }
                        else if (grid[sy][sx] != targetChars[si])
                        {
                            found = false;
                        }
                        else
                        {
                            si++;
                            sx += dir.dx;
                            sy += dir.dy;
                        }
                    } while (found && si < targetChars.Length);

                    if (!found)
                    {
                        seqSatisfied = false;
                        break;
                    }
                }

                if (seqSatisfied)
                {
                    yield return (x, y);
                }
            }
        }
    }
}

var count = FindInstancesOf("XMAS", false).Count();
Console.WriteLine("Found {0} instances of XMAS in wordsearch.", count);

count = FindInstancesOf("MAS", true).Count();
Console.WriteLine("Found {0} instances of X-MAS in wordsearch.", count);
