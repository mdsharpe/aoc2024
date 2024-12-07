using day7;

var lines = await File.ReadAllLinesAsync(args[0]);
var equations = lines.Select(o => Equation.Parse(o)).ToArray();
