using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Domain
{
    public class Function
    {
        public FunctionType Type { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
    }

    public enum FunctionType
    {
        Smallest,
        Largest,
        IsNull
    }

    public static class RuleFunction
    {
        private static Dictionary<FunctionType, Function> _functionPairs = new Dictionary<FunctionType, Function>()
        {
            { FunctionType.Smallest, new Function()  { Type = FunctionType.Smallest,  Format = "Smallest({items})", Description = "Returns the smallest item of a list" } },
            { FunctionType.Largest,  new Function()  { Type = FunctionType.Largest,   Format = "Largest({items})",  Description = "Returns the largest item of a list" } },
            { FunctionType.IsNull,   new Function()  { Type = FunctionType.IsNull,    Format = "IsNull({item})",    Description = "Returns true/false if an item is null or empty"} }
        };

        public static Dictionary<FunctionType, Function> GetAll()
        {
            return _functionPairs;
        }

        public static Function GetFunction(FunctionType type)
        {
            return _functionPairs.FirstOrDefault(x => x.Key == type).Value;
        }

        public static string GetFormat(FunctionType type)
        {
            return GetFunction(type).Format;
        }

        public static string GetDecription(FunctionType type)
        {
            return GetFunction(type).Description;
        }
    }

}
