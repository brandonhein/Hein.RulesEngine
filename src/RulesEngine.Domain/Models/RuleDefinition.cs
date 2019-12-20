using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Domain.Models
{
    public class RuleDefinition
    {
        public Guid DefintionId { get; set; }
        public string Name { get; set; }
        public List<EntityProperty> Properties { get; set; }
        public List<Rule> Rules { get; set; }

        public Rule LatestChangedRule()
        {
            return Rules
                .OrderByDescending(x => x.ChangeHistory.OrderByDescending(c => c.DateTime))
                .FirstOrDefault();
        }
    }
}
