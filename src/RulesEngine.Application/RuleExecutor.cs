using Hein.RulesEngine.Domain.Magic;
using Hein.RulesEngine.Domain.Models;
using Hein.RulesEngine.Framework.Extensions;
using System;
using System.Collections.Generic;

namespace Hein.RulesEngine.Application
{
    public class RuleExecutor
    {
        private readonly IEnumerable<EntityProperty> _properties;
        private readonly Dictionary<string, string> _parameters;
        public RuleExecutor(IEnumerable<EntityProperty> properties, Dictionary<string, string> parameters)
        {
            _properties = properties;
            _parameters = parameters;
        }

        public RuleTracker Run(Rule rule)
        {
            var tracker = new RuleTracker()
            {
                Name = rule.Name,
                Priority = rule.Priority
            };

            try
            {
                var code = DynamicCode.Build(_properties, rule.Conditions, _parameters);
                tracker.Passed = code.Execute<bool>();
            }
            catch (Exception ex)
            {
                tracker.Passed = false;
            }

            var results = new Dictionary<string, object>();
            foreach (var parameter in _parameters)
            {
                results.Add(parameter.Key, parameter.Value);
            }

            if (tracker.Passed)
            {
                foreach (var property in rule.Results)
                {
                    if (results.ContainsKey(property.Name))
                    {
                        results.Remove(property.Name);
                    }

                    results.Add(property.Name, property.Value.ToType(property.Type));
                }
            }

            tracker.Results = results;

            return tracker;
        }
    }
}
