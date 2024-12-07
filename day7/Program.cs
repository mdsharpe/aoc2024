using day7;

var lines = await File.ReadAllLinesAsync(args[0]);
var equations = lines.Select(o => Equation.Parse(o)).ToArray();

var totalCalibrationResult = 0;

foreach (var equation in equations)
{
    var operatorPermutations = OperatorPermutations.Generate(equation.Numbers.Length - 1);
    foreach (var operatorPermutation in operatorPermutations)
    {
        if (equation.TrySolve(operatorPermutation))
        {
            totalCalibrationResult += equation.TestValue;
        }
    }
}

Console.WriteLine("Total calibration result: {0}", totalCalibrationResult);
