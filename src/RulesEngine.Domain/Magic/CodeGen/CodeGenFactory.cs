using Hein.RulesEngine.Domain.Models;

namespace Hein.RulesEngine.Domain.Magic.CodeGen
{
    public static class CodeGenFactory
    {
        public static IGenerateConditionalCode ConditionalCode(string operatorType)
        {
            switch (operatorType.ToLower())
            {
                case "isoneof":
                    return new IsOneOfGenerator();
                case "isnull":
                    return new IsNullGenerator();
                case "isnotnull":
                    return new IsNotNullGenerator();
                case ">":
                case "isgreaterthan":
                    return new GreaterThanGenerator();
                case "<":
                case "islessthan":
                    return new LessThanGenerator();
                case ">=":
                case "isgreaterthanorequal":
                    return new GreaterThanOrEqualGenerator();
                case "<=":
                case "islessthanorequal":
                    return new LessThanOrEqualGenerator();
                case "=":
                case "==":
                case "isequal":
                case "isequalto":
                    return new IsEqualToGenerator();
                case "!=":
                case "isnotequal":
                case "isnotequalto":
                    return new IsNotEqualToGenerator();
            }

            return null;
        }


    }

    public interface IGenerateConditionalCode
    {
        string Generate(EntityProperty property, object parameter, string comparisons);
    }
}
