using Hein.RulesEngine.Domain.Magic.CodeGen;
using Hein.RulesEngine.Domain.Models;
using Hein.RulesEngine.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Domain.Magic
{
    public static class DynamicCode
    {
        public static string Build(IEnumerable<EntityProperty> properties, IEnumerable<RuleParameters> conditions, Dictionary<string, object> parameters)
        {
            var result = string.Empty;

            foreach (var condition in conditions)
            {
                var property = properties.FirstOrDefault(x => x.Name == condition.Property);
                var parameter = parameters.FirstOrDefault(x => x.Key == condition.Property);

                if (property != null)
                {
                    var propertyResult = CodeGenFactory
                        .ConditionalCode(condition.Operator)
                        .Generate(property, parameter.Value, condition.Value);

                    result = string.Concat(result, " && ", propertyResult);
                }
            }

            //get rid of the last " && "
            result = result.Trim();
            result = result.TrimEnd('&');
            result = result.Trim();

            RulesEngineLogger.Debug("Condition Code Generated: " + result);
            return result;
        }
    }
}
