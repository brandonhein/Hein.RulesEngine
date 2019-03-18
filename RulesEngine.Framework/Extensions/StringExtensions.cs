using System;

namespace Hein.RulesEngine.Framework.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets string between other strings/characters
        /// </summary>
        /// <param name="strSource">Whole String</param>
        /// <param name="strStart">Text to find at the start</param>
        /// <param name="strEnd">Text to find at the end</param>
        /// <returns>String found between Start and End</returns>
        public static string Between(this string strSource, string strStart, string strEnd)
        {
            if (string.IsNullOrEmpty(strSource))
            {
                return string.Empty;
            }

            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

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
