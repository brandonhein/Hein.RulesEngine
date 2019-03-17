using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Framework.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsOneOf<T>(this T source, IEnumerable<T> values)
        {
            return IsOneOf(source, values.ToArray());
        }

        public static bool IsOneOf<T>(this T source, params T[] comparisonValue)
        {
            bool exists = false;

            if (source != null && comparisonValue != null && comparisonValue.Any())
            {
                exists = comparisonValue.Contains(source);
            }

            return exists;
        }
    }
}
