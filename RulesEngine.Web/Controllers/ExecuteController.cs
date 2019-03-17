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
            //var json = "{\"Creator\":\"Brandon\",\"Default\":{\"Actions\":\"Set(IsRefi, false);\",\"Condition\":\"true\",\"Name\":\"Default\",\"Priority\":1616},\"Entity\":{\"Name\":\"LoanPurposeEntity\",\"Properties\":[{\"Name\":\"LoanPurpose\",\"Required\":true,\"Type\":\"String\"},{\"Name\":\"IsRefi\",\"Required\":false,\"Type\":\"Bool\"}]},\"Id\":\"4b7aa2a4-1593-4c71-9616-58e00fb4d7f0\",\"LastUpdateDate\":\"2019-03-16T22:40:00Z\",\"LastUpdatedBy\":\"Brandon\",\"Name\":\"LoanPurposeRule\",\"Rules\":[{\"Actions\":\"Set(IsRefi, true);\",\"Condition\":\"#LoanPurpose#.ToLower() == 'Refinance'.ToLower() || #LoanPurpose#.ToLower() == 'Refi'.ToLower()\",\"Name\":\"RefiRule1\",\"Priority\":1}]}";
            var json = "{\"Creator\":\"Brandon\",\"Default\":null,\"Entity\":{\"Name\":\"AlphaLeadStatus\",\"Properties\":[{\"Name\":\"EventId\",\"Required\":true,\"Type\":\"Number\"},{\"Name\":\"StatusId\",\"Required\":false,\"Type\":\"Number\"},{\"Name\":\"IsCallEvent\",\"Required\":false,\"Type\":\"Bool\"},{\"Name\":\"CallDuration\",\"Required\":false,\"Type\":\"Decimal\"}]},\"Id\":\"c175870c-9ede-4019-9e0e-432512c40bb0\",\"LastUpdateDate\":\"2019-03-16T22:40:00Z\",\"LastUpdatedBy\":\"Brandon\",\"Name\":\"AlphaLeadStatusRule\",\"Rules\":[{\"Actions\":\"Set(StatusId, #EventId#);\",\"Condition\":\"#EventId#.IsOneOf(1010,1020,1021,1100,1110,3020,3021,3025)\",\"Name\":\"StatusRule\",\"Priority\":1},{\"Condition\":\"#IsCallEvent# && #CallDuration# < 127 && #StatusId#.IsOneOf(3005,3010)\",\"Name\":\"Incoming_call_not_long_enough_for_convo\",\"Actions\":\"Set(StatusId, 3010);\",\"Priority\":2},{\"Condition\":\"#IsCallEvent# && #CallDuration# >= 127 && #StatusId#.IsOneOf(3005,3010)\",\"Name\":\"Incoming_call_long_enough_for_convo\",\"Actions\":\"Set(StatusId, 3015);\",\"Priority\":3}]}";

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
