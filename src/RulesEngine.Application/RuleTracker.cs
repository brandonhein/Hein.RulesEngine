using System.Collections.Generic;

namespace Hein.RulesEngine.Application
{
    public class RuleTracker
    {
        public string Name { get; set; }
        public decimal Priority { get; set; }
        public bool Passed { get; set; }
        public Dictionary<string, object> Results { get; set; }
    }
}
