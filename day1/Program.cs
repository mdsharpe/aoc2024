var lines = await File.ReadAllLinesAsync(args[0]);

var count = 0;
List<int> left = [], right = [];

foreach (var line in lines)
{
    var nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

    left.Add(nums[0]);
    right.Add(nums[1]);
    count++;
}

left.Sort();
right.Sort();

var totalDist = (from i in Enumerable.Range(0, count)
                 let l = left[i]
                 let r = right[i]
                 select Math.Abs(l - r)).Sum();

Console.WriteLine("Total distance: {0}", totalDist);

var rightCounts = right.GroupBy(o => o)
    .ToDictionary(o => o.Key, o => o.Count());

var totalSimilarity = (from n in left
                       where rightCounts.ContainsKey(n)
                       let c = rightCounts[n]
                       select n * c).Sum();

Console.WriteLine("Total similarity: {0}", totalSimilarity);
