using Microsoft.AspNetCore.Mvc;
using Hein.RulesEngine.Application.Execution;
using Hein.Framework.Extensions;

namespace Hein.RulesEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var code = "\"3005\".IsOneOf(\"3005\",\"3010\")";
            var result = code.Execute<bool>();

            var extResult = "3005".IsOneOf("3005", "3010");

            return Ok(result);
        }
    }
}
