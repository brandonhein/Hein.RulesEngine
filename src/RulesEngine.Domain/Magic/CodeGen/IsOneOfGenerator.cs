using Hein.RulesEngine.Domain.Models;

namespace Hein.RulesEngine.Domain.Magic.CodeGen
{
    public class IsOneOfGenerator : IGenerateConditionalCode
    {
        public string Generate(EntityProperty property, object parameter, string comparisons)
        {
            var comparionsArray = comparisons.Split(',');
            var isOneOfParam = string.Empty;

            //need to add " if its a string
            if (property.Type.ToLower() == "string")
            {
                foreach (var compare in comparionsArray)
                {
                    isOneOfParam = $"{isOneOfParam} \"{compare}\",";
                }

                isOneOfParam = isOneOfParam.TrimEnd(',');

                return $" \"{parameter}\".IsOneOf({isOneOfParam}) ";
            }


            //For numbers or literally anything else
            foreach (var compare in comparionsArray)
            {
                isOneOfParam = $"{isOneOfParam} {compare},";
            }

            isOneOfParam = isOneOfParam.TrimEnd(',');

            return $" {parameter}.IsOneOf({isOneOfParam}) ";
        }
    }
}
