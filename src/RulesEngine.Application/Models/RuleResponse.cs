using System.Collections.Generic;

namespace Hein.RulesEngine.Application.Models
{
    public class RuleResponse
    {
        public string Rule { get; set; }
        public string Winner { get; set; }
        public Dictionary<string, object> Values { get; set; }
    }
}
