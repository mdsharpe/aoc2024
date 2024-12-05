var lines = await File.ReadAllLinesAsync(args[0]);

(int x, int y)[] pageOrderingRules =
    (from line in lines.TakeWhile(o => o.Length > 0)
     let separatorIndex = line.IndexOf('|')
     let x = int.Parse(line.Substring(0, separatorIndex))
     let y = int.Parse(line.Substring(separatorIndex + 1, line.Length - (separatorIndex + 1)))
     select (x, y)).ToArray();

int[][] pagesToProduceInEachUpdate =
    (from line in lines.Skip(pageOrderingRules.Length + 1)
     select line.Split(',').Select(int.Parse).ToArray()).ToArray();

var comparer = new Comparer(pageOrderingRules);

bool GetIsInOrder(int[] update)
{
    Console.Write(string.Join(',', update));
    Console.Write(" => ");
    var sorted = update.OrderBy(o => o, comparer).ToArray();
    Console.Write(string.Join(',', sorted));
    var isInOrder = update.SequenceEqual(sorted);
    Console.WriteLine(isInOrder ? " : Y" : " : N");

    return isInOrder;
}

var result = 0;

foreach (var update in pagesToProduceInEachUpdate)
{
    if (GetIsInOrder(update))
    {
        result += update[update.Length / 2];
    }
}

Console.WriteLine("Part 1 result: {0}", result);

class Comparer((int x, int y)[] rules) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        var matchingRule = rules.FirstOrDefault(o => o.x == x && o.y == y || o.x == y && o.y == x);

        if (matchingRule.x == x && matchingRule.y == y)
        {
            return -1;
        }

        if (matchingRule.x == y && matchingRule.y == x)
        {
            return 1;
        }

        return 0;
    }
}
