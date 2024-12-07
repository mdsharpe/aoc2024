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
        var numbers = s.Substring(s.IndexOf(':'))
            .Split(' ')
            .Select(int.Parse);
        return new Equation(testValue, numbers);
    }
}
