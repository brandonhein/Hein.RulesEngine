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
            switch (toType)
            {
                case "string":
                    return obj.ToType<string>();
            }

            return null;
        }
    }
}
