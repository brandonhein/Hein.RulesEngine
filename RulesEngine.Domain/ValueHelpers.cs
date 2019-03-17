using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Domain
{
    public class ValueHelper
    {
        public ValueHelperType Type { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
    }

    public enum ValueHelperType
    {
        IsOneOf,
        IsNull,
        IsNotNull,
        Contains,
        ToLower,
        ToUpper,
    }

    public static class RuleHelper
    {
        private static Dictionary<ValueHelperType, ValueHelper> _helperPairs = new Dictionary<ValueHelperType, ValueHelper>()
        {
            { ValueHelperType.IsOneOf,   new ValueHelper()  { Type = ValueHelperType.IsOneOf,   Format = "{item}.IsOneOf({items})",    Description = "Returns true/false if an item is one of items in a list" } },
            { ValueHelperType.IsNull,    new ValueHelper()  { Type = ValueHelperType.IsNull,    Format = "{item}.IsNull()",            Description = "Returns true/false if an item is null or empty"} },
            { ValueHelperType.IsNotNull, new ValueHelper()  { Type = ValueHelperType.IsNotNull, Format = "{item}.IsNotNull()",         Description = "Returns true/false if an item is not null or empty"} },
            { ValueHelperType.Contains,  new ValueHelper()  { Type = ValueHelperType.Contains,  Format = "{item}.Contains({lookup}})", Description = "Returns true/false if an item contains a string squence" } },
            { ValueHelperType.ToLower,   new ValueHelper()  { Type = ValueHelperType.ToLower,   Format = "{item}.ToLower()",           Description = "Returns an all lower case of an item" } },
            { ValueHelperType.ToUpper,   new ValueHelper()  { Type = ValueHelperType.ToUpper,   Format = "{item}.ToUpper()",           Description = "Returns an all upper case of an item" } }
        };

        public static Dictionary<ValueHelperType, ValueHelper> GetAll()
        {
            return _helperPairs;
        }

        public static ValueHelper GetHelper(ValueHelperType type)
        {
            return _helperPairs.FirstOrDefault(x => x.Key == type).Value;
        }

        public static string GetFormat(ValueHelperType type)
        {
            return GetHelper(type).Format;
        }

        public static string GetDecription(ValueHelperType type)
        {
            return GetHelper(type).Description;
        }
    }
}
