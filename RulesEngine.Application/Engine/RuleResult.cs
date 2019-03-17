using System.Collections.Generic;

namespace Hein.RulesEngine.Application.Engine
{
    public class RuleResult
    {
        public string RuleName { get; set; }
        public IDictionary<string, object> Results { get; set; }
    }

    public class RuleTracker
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool Passed { get; set; }
        public IDictionary<string, object> Results { get; set; }
    }
}
