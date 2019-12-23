using System.Collections.Generic;

namespace Hein.RulesEngine.Application.Models
{
    public class RuleRequest
    {
        public string Rule { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
