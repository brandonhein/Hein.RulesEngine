using Hein.RulesEngine.Domain;
using Hein.RulesEngine.Domain.Models;
using Hein.RulesEngine.Framework.Extensions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Application.Engine
{
    public class RuleExecutor
    {
        private readonly List<EntityProperty> _properites;
        private IDictionary<string, object> _parameters;
        private Dictionary<string, object> result;
        public RuleExecutor(IEnumerable<EntityProperty> properties, IDictionary<string, object> parameters)
        {
            _properites = properties.ToList();
            _parameters = parameters;
            result = new Dictionary<string, object>();
        }

        public RuleTracker Run(Rule rule)
        {
            if (!string.IsNullOrEmpty(rule.Setups))
            {
                var setupSteps = rule.Setups.Trim().Split(";");
                foreach (var step in setupSteps)
                {
                    if (step.Trim().StartsWith("Add("))
                    {
                        var propName = step.Between("Add(", ",").Replace("'", "").Replace("\"", "").Trim();
                        var valueType = step.Between(",", ")").Trim();
                        _properites.Add(new EntityProperty() { Name = propName, Type = valueType });
                    }
                }
            }

            bool passed = false;
            if (!string.IsNullOrEmpty(rule.Condition))
            {
                rule.Condition = rule.Condition.Replace("'", "\"");

                foreach (var parameter in _parameters)
                {
                    var property = _properites.FirstOrDefault(x => x.Name == parameter.Key);
                    if (property.Type.IsOneOf("String", "string"))
                    {
                        rule.Condition = rule.Condition.Replace($"#{parameter.Key}#", $"\"{parameter.Value}\"");
                    }
                    else
                    {
                        rule.Condition = rule.Condition.Replace($"#{parameter.Key}#", $"{parameter.Value}");
                    }
                }

                passed = CSharpScript.EvaluateAsync<bool>(rule.Condition).Result;
            }

            foreach (var property in _properites)
            {
                var parameter = _parameters.FirstOrDefault(x => x.Key == property.Name);
                if (!parameter.IsNullOrEmpty())
                {
                    result.Add(property.Name, parameter.Value);
                }
                else
                {
                    result.Add(property.Name, RuleType.GetType(property.Type).GetDefault());
                }
            }

            if (passed)
            {
                if (!string.IsNullOrEmpty(rule.Actions))
                {
                    var actionSteps = rule.Actions.Trim().Split(";");
                    foreach (var step in actionSteps)
                    {
                        if (step.Trim().StartsWith("Set("))
                        {
                            var propName = step.Between("Set(", ",").Replace("'", "").Replace("\"", "").Trim();
                            var valueToSet = step.Between(",", ")").Trim();

                            var resultItem = result.FirstOrDefault(x => x.Key == propName);
                            var property = _properites.FirstOrDefault(x => x.Name == propName);
                            if (!resultItem.IsNullOrEmpty())
                            {
                                var value = Converter.ChangeType(RuleType.GetType(property.Type), valueToSet);

                                result.Remove(propName);
                                result.Add(propName, value);
                            }
                        }
                    }
                }
            }

            return new RuleTracker() { Passed = passed, Priority = rule.Priority, Results = result, Name = rule.Name };
        }
    }
}
