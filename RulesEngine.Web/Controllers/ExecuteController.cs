using Hein.RulesEngine.Application.Engine;
using Hein.RulesEngine.Application.Models;
using Hein.RulesEngine.Domain.Models;
using Hein.RulesEngine.Framework.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Hein.RulesEngine.Web.Controllers
{
    [Route("Rules")]
    public class ExecuteController : Controller
    {
        [HttpPost]
        public IActionResult Execute([FromBody] RuleRequest request)
        {
            var json = "{\"Creator\":\"Brandon\",\"Entity\":{\"Name\":\"LoanPurposeEntity\",\"Properties\":[{\"Name\":\"LoanPurpose\",\"Required\":true,\"Type\":\"String\"}]},\"Id\":\"4b7aa2a4-1593-4c71-9616-58e00fb4d7f0\",\"LastUpdateDate\":\"2019-03-16T22:40:00Z\",\"LastUpdatedBy\":\"Brandon\",\"Name\":\"SampleRule1\",\"Rules\":[{\"Condition\":\"#LoanPurpose#.ToLower() == 'Refinance'.ToLower() || #LoanPurpose#.ToLower() == 'Refi'.ToLower()\",\"Name\":\"RefiRule1\",\"Priority\":1,\"Actions\":\"Set(IsRefi, true);\",\"Setups\":\"Add(IsRefi, bool);\"}],\"Default\":{\"Condition\":\"true\",\"Name\":\"Default\",\"Priority\":1616,\"Actions\":\"Set(IsRefi, false);\",\"Setups\":\"Add(IsRefi, bool);\"}}";

            var def = Deserialize.JsonToObject<RuleDefinition>(json);

            RuleResult result;
            using (var runner = new RuleRunner(def))
            {
                runner.ApplyRules(request.Parameters);

                result = runner.Result;
            }

            return Ok(result);
        }
    }


}
