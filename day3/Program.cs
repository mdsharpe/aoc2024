using System.Text.RegularExpressions;

var regexMul = new Regex(@"(?>mul\((\d+),(\d+)\)|do\(\)|don't\(\))");

var memory = await File.ReadAllTextAsync(args[0]);

var matches = regexMul.Matches(memory);

var total = 0;
var totalWithConditionals = 0;
var mulEnabled = true;

foreach (Match match in matches)
{
    if (match.Value == "do()")
    {
        mulEnabled = true;
        continue;
    }

    if (match.Value == "don't()")
    {
        mulEnabled = false;
        continue;
    }

    var a = int.Parse(match.Groups[1].Value);
    var b = int.Parse(match.Groups[2].Value);
    var result = a * b;

    total += result;

    if (mulEnabled)
    {
        totalWithConditionals += result;
    }
}

Console.WriteLine("Total: {0}", total);
Console.WriteLine("Total (with conditionals): {0}", totalWithConditionals);
