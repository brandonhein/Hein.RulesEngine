using Microsoft.AspNetCore.Mvc;
using Hein.RulesEngine.Application.Execution;
using Hein.Framework.Extensions;

namespace Hein.RulesEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string id)
        {
            var code = string.Format("\"{0}\".IsOneOf(\"3005\",\"3010\")", id);
            var result = code.Execute<bool>();

            var extResult = id.IsOneOf("3005", "3010");

            return Ok(result);
        }
    }
}
