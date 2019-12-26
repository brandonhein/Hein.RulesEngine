using Hein.RulesEngine.Framework;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Domain.Magic
{
    public static class DynamicCodeExecution
    {
        public static bool TryExecute<T>(this string cSharpCode, out T result)
        {
            try
            {
                result = Execute<T>(cSharpCode);
                return true;
            }
            catch (Exception ex)
            {
                RulesEngineLogger.Error("Dynamic Code Execution Error", ex.InnerException);
                result = default(T);
                return false;
            }
        }

        public static T Execute<T>(this string cSharpCode)
        {
            return ExecuteAsync<T>(cSharpCode).Result;
        }

        public static async Task<T> ExecuteAsync<T>(this string cSharpCode)
        {
            var assemblies = ScriptOptions.Default;
            //WithImports = "using System.Math" at the top of the script
            assemblies = assemblies.WithImports(
                "System.Math",
                "Hein.Framework.Extensions");
            //WithReferences = pulling in actual .dlls outside of System
            assemblies = assemblies.WithReferences(
                typeof(Hein.Framework.Extensions.GenericExtensions).Assembly);

            var result = await CSharpScript.EvaluateAsync<T>(cSharpCode, assemblies);

            return result;
        }
    }
}
