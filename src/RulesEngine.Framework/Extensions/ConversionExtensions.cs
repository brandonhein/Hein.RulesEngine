using Hein.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.RulesEngine.Framework.Extensions
{
    public static class ConversionExtensions
    {
        public static object ToType(this object obj, string toType)
        {
            switch (toType.ToLower())
            {
                case "string":
                    return obj.ToType<string>();
                case "bool":
                case "boolean":
                    return obj.ToString().ToLower().ToType<bool>();
            }

            return null;
        }
    }
}
