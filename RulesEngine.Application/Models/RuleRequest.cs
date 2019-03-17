using System.Collections.Generic;

namespace Hein.RulesEngine.Application.Models
{
    public class RuleRequest
    {
        public string RuleSet { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
    }
}
