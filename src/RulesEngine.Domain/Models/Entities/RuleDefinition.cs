using Hein.Framework.Dynamo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Domain.Models
{
    public class RuleDefinition : IEntity
    {
        public Guid DefintionId { get; private set; }
        public string Name { get; set; }
        public List<EntityProperty> Properties { get; set; }
        public List<Rule> Rules { get; set; }

        public Guid GetId()
        {
            return DefintionId;
        }

        public void SetId(Guid id)
        {
            DefintionId = id;
        }

        public Rule LatestChangedRule()
        {
            return Rules
                .OrderByDescending(x => x.ChangeHistory.OrderByDescending(c => c.DateTime))
                .FirstOrDefault();
        }

        public void ExecuteAfterGet()
        {
            //ope
        }

        public void ExecuteAfterSave()
        {
            //ope
        }

        public void ExecuteBeforeSave()
        {
            //ope
        }
    }
}
