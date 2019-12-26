using Hein.Framework.Extensions;
using Hein.RulesEngine.Domain.Models;

namespace Hein.RulesEngine.Domain.Magic.CodeGen
{
    public class IsNotEqualToGenerator : IGenerateConditionalCode
    {
        public string Generate(EntityProperty property, object parameter, string comparisons)
        {
            if (property.Type.ToLower() == "string")
            {
                return $" \"{parameter.ToString().StandardizeForCompare()}\" != \"{comparisons.StandardizeForCompare()}\" ";
            }

            return $" {parameter} == {comparisons} ";
        }
    }
}
