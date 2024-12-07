using System;
using System.Collections.Immutable;

namespace day7;

public class Equation
{
    public Equation(int testValue, IEnumerable<int> numbers)
    {
        TestValue = testValue;
        Numbers = numbers.ToImmutableArray();
    }

    public int TestValue { get; }
    public ImmutableArray<int> Numbers { get; }

    public static Equation Parse(string s)
    {
        var testValue = int.Parse(s.Substring(0, s.IndexOf(':')));
        var numbers = s.Substring(s.IndexOf(':') + 1)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);
        return new Equation(testValue, numbers);
    }

    public bool TrySolve(IList<OperatorKind> operators, out int result)
    {
        if (operators.Count != Numbers.Length - 1)
        {
            throw new ArgumentException("Incorrect number of operators.");
        }

        result = Numbers[0];

        for (var i = 0; i < operators.Count; i++)
        {
            var n = Numbers[i + 1];

            switch (operators[i])
            {
                case OperatorKind.Add:
                    result += n;
                    break;
                case OperatorKind.Multiply:
                    result *= n;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        return result == TestValue;
    }
}
