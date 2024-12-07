using System;
using System.Collections.Immutable;

namespace day7;

public class Equation
{
    public Equation(long testValue, IEnumerable<long> numbers)
    {
        TestValue = testValue;
        Numbers = numbers.ToImmutableArray();
    }

    public long TestValue { get; }
    public ImmutableArray<long> Numbers { get; }

    public static Equation Parse(string s)
    {
        var testValue = long.Parse(s.Substring(0, s.IndexOf(':')));
        var numbers = s.Substring(s.IndexOf(':') + 1)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse);
        return new Equation(testValue, numbers);
    }

    public bool TrySolve(IList<OperatorKind> operators, out long result)
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
                case OperatorKind.Concatenante:
                    result = long.Parse(result.ToString() + n.ToString());
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        return result == TestValue;
    }
}
