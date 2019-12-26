using System;
using System.Collections.Generic;

namespace Hein.RulesEngine.Domain.Models
{
    public class Rule
    {
        public Guid RuleId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public decimal Priority { get; set; }
        public List<RuleParameters> Conditions { get; set; }
        public List<EntityPropertyResult> Results { get; set; }

        public List<History> ChangeHistory { get; set; }
    }
}
