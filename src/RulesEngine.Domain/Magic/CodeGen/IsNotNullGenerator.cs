using Hein.RulesEngine.Domain.Models;

namespace Hein.RulesEngine.Domain.Magic.CodeGen
{
    public class IsNotNullGenerator : IGenerateConditionalCode
    {
        public string Generate(EntityProperty property, object parameter, string comparisons)
        {
            if (property.Type.ToLower() == "string")
            {
                return $" !string.IsNullOrEmpty(\"{parameter}\") ";
            }

            return $" {parameter} != null ";
        }
    }
}
