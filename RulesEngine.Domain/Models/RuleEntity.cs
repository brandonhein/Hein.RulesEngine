using System.Collections.Generic;

namespace Hein.RulesEngine.Domain.Models
{
    public class Entity
    {
        public string Name { get; set; }
        public IEnumerable<EntityProperty> Properties { get; set; }
    }

    public class EntityProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
    }
}
