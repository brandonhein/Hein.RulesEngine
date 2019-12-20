using Hein.RulesEngine.Domain.Models;
using System.Collections.Generic;

namespace Hein.RulesEngine.Domain.Magic
{
    public static class DynamicCode
    {
        public static string Build(IEnumerable<EntityProperty> properties, IEnumerable<RuleParameters> conditions)
        {
            var result = string.Empty;

            foreach (var condtion in conditions)
            {

                result = string.Concat(result, " && ");
            }

            //get rid of the last " && "
            result = result.Trim();
            result = result.TrimEnd('&');
            result = result.Trim();
            return result;
        }
    }
}
