using System;
using System.Collections.Generic;

namespace Hein.RulesEngine.Domain.Models
{
    public class RuleDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public Entity Entity { get; set; }

        public List<Rule> Rules { get; set; }

        public Rule Default { get; set; }
    }
}
