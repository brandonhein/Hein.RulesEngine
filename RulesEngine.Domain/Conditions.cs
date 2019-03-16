using System.Collections.Generic;

namespace Hein.RulesEngine.Domain
{
    public enum ConditionType
    {
        And,
        Or,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo
    }

    public static class RuleCondition
    {
        private static Dictionary<ConditionType, string> _conditionPairs = new Dictionary<ConditionType, string>()
        {
            { ConditionType.And, "&&" },
            { ConditionType.Or, "||" },
            { ConditionType.Equal, "==" },
            { ConditionType.NotEqual, "!=" },
            { ConditionType.LessThan, "<" },
            { ConditionType.LessThanOrEqualTo, "<=" },
            { ConditionType.GreaterThan, ">" },
            { ConditionType.GreaterThanOrEqualTo, ">=" }
        };
    }
}
