using day7;

var lines = await File.ReadAllLinesAsync(args[0]);
var equations = lines.Select(o => Equation.Parse(o)).ToArray();

long totalCalibrationResult = 0;

foreach (var equation in equations)
{
    var operatorPermutations = OperatorPermutations.Generate(equation.Numbers.Length - 1);

    foreach (var operators in operatorPermutations)
    {
        if (equation.TrySolve(operators, out var result))
        {
            totalCalibrationResult += equation.TestValue;
            break;
        }
    }
}

Console.WriteLine("Total calibration result: {0}", totalCalibrationResult);
