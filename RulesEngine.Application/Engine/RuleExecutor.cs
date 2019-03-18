using Hein.RulesEngine.Domain;
using Hein.RulesEngine.Domain.Models;
using Hein.RulesEngine.Framework.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                var condition = rule.Condition.ReplaceValuesWithParameters(_properites, _parameters);
                var options = ScriptOptions.Default
                    .AddReferences(typeof(GenericExtensions).Assembly)
                    .WithImports("Hein.RulesEngine.Framework.Extensions");

                //var r = 3005.IsOneOf(3005, 3010);

                try
                {
                    passed = CSharpScript.EvaluateAsync<bool>(condition, options)
                        .Result;
                }
                catch (Exception ex)
                {
                    passed = false;
                }
            }

            //set results with default values of entity properties
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
                    rule.Actions = rule.Actions.ReplaceValuesWithParameters(_properites, result);

                    var actionSteps = rule.Actions.Trim().Split(";");
                    foreach (var step in actionSteps)
                    {
                        if (step.Trim().StartsWith("Set("))
                        {
                            var propName = step.Between("Set(", ",").Replace("'", "").Replace("\"", "").Trim();
                            var valueToSet = step.Between(",", ")").Trim();

                            var resultItem = result.FirstOrDefault(x => x.Key == propName);
                            var property = _properites.FirstOrDefault(x => x.Name == propName);
                            if (!resultItem.IsNullOrEmpty() || (property != null && property.Required))
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
