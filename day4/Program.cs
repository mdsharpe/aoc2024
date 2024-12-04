var grid = (await File.ReadAllLinesAsync(args[0]))
    .Select(o => o.ToCharArray())
    .ToArray();

var width = grid.Select(o => o.Length).Max();
var height = grid.Length;

(int dx, int dy)[] searchDirs = [
    (1, 0),
    (1, 1),
    (0, 1),
    (-1, 1),
    (-1, 0),
    (-1, -1),
    (0, -1),
    (1, -1)
];

IEnumerable<(int x, int y)> FindInstancesOf(string word)
{
    var targetChars = word.ToCharArray();

    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            foreach (var dir in searchDirs)
            {
                int sx = x, sy = y, si = 0;
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

                if (found)
                {
                    yield return (x, y);
                }
            }
        }
    }
}

var count = FindInstancesOf("XMAS").Count();
Console.WriteLine("Found {0} instances of XMAS in wordsearch.", count);
