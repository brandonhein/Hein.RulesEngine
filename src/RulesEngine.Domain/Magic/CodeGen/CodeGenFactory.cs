using Hein.RulesEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.RulesEngine.Domain.Magic.CodeGen
{
    public static class CodeGenFactory
    {
        public static IGenerateConditionalCode ConditionalCode()
        {

        }


    }

    public interface IGenerateConditionalCode
    {
        string Generate(EntityProperty property, RuleParameters condition);
    }
}
