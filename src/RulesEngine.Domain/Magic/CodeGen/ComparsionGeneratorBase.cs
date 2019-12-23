using Hein.RulesEngine.Domain.Models;

namespace Hein.RulesEngine.Domain.Magic.CodeGen
{
    public class ComparsionGeneratorBase : IGenerateConditionalCode
    {
        private readonly string _operator;
        public ComparsionGeneratorBase(string operatorType)
        {
            _operator = operatorType;
        }

        public string Generate(EntityProperty property, object parameter, string comparisons)
        {
            return $" {comparisons} {_operator} {parameter} ";
        }
    }
}
