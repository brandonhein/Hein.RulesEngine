using System.Collections.Generic;

namespace Hein.RulesEngine.Application.Models
{
    public class RuleRequest
    {
        public string Rule { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
