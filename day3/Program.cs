using System.Text.RegularExpressions;

var regexMul = new Regex(@"mul\((\d+),(\d+)\)");

var memory = await File.ReadAllTextAsync(args[0]);

var matches = regexMul.Matches(memory);

var total = 0;

foreach (Match match in matches)
{
    Console.WriteLine(match);
    var a = int.Parse(match.Groups[1].Value);
    var b = int.Parse(match.Groups[2].Value);
    var result = a * b;
    total += result;
}

Console.WriteLine("Total: {0}", total);
