using Microsoft.AspNetCore.Mvc;

namespace Hein.RulesEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
