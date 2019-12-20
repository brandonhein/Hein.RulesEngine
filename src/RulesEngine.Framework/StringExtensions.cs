using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.RulesEngine.Framework
{
    public static class StringExtensions
    {
        public static bool IsOneOf(this string val, params string[] comparisonValue)
        {
            if (!string.IsNullOrEmpty(val))
            {
                foreach (var s in comparisonValue)
                {
                    if (!string.IsNullOrEmpty(s) &&
                        s.Equals(val, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
