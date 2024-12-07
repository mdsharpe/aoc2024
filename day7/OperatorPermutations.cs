using System;
using System.Collections.Immutable;

namespace day7;

public static class OperatorPermutations
{
    public static IEnumerable<List<OperatorKind>> Generate(int count)
    {
        var foo = count > 1 ? Generate(count - 1) : [[]];

        foreach (var operatorKind in Enum.GetValues<OperatorKind>())
        {
            foreach (var bar in foo)
            {
                yield return bar.Concat([operatorKind]).ToList();
            }
        }
    }
}
