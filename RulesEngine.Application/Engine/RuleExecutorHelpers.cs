using Hein.RulesEngine.Domain.Models;
using Hein.RulesEngine.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hein.RulesEngine.Application.Engine
{
    public static class RuleExecutorHelpers
    {
        public static string ReplaceValuesWithParameters(this string item, IEnumerable<EntityProperty> properties, IDictionary<string, object> parameters)
        {
            item = item.Replace("'", "\"");

            foreach (var parameter in parameters)
            {
                var property = properties.FirstOrDefault(x => x.Name == parameter.Key);
                if (property.Type.IsOneOf("String", "string"))
                {
                    item = item.Replace($"#{parameter.Key}#", $"\"{parameter.Value}\"");
                }
                else
                {
                    item = item.Replace($"#{parameter.Key}#", $"{parameter.Value}");
                }
            }

            return item;
        }

        public static string ConvertValueHelpers(this string condition)
        {
            if (condition.Contains("IsOneOf("))
            {
                var isOneOfCondition = string.Concat(condition.Between(" ", ")"), ")");
                if (isOneOfCondition == ")")
                {
                    var temp = string.Concat("{Start}", condition);
                    isOneOfCondition = string.Concat(temp.Between("{Start}", ")"), ")");
                }

                var items = isOneOfCondition.Between("(", ")").Split(",");
                var thisItem = string.Concat("{Start}", isOneOfCondition).Between("{Start}", ".IsOneOf(");

                var sb = new StringBuilder();

                foreach (var item in items)
                {
                    sb.Append(string.Concat(thisItem, " == ", item));
                    sb.Append(" || ");
                }

                char[] charsToTrim = { ' ', '|'};
                var convertedString = string.Concat("(", sb.ToString().TrimEnd(charsToTrim), ")");

                return condition.Replace(isOneOfCondition, convertedString);
            }

            return condition;
        }
    }
}
