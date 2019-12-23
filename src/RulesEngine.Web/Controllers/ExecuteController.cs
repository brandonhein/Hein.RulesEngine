using Hein.Framework.Dynamo;
using Hein.Framework.Http;
using Hein.RulesEngine.Application;
using Hein.RulesEngine.Application.Models;
using Hein.RulesEngine.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Web.Controllers
{
    [Route("Execute")]
    public class ExecuteController : Controller
    {
        private readonly IRepositoryContext _context;
        public ExecuteController(IRepositoryContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Consumes(HttpContentType.Json)]
        [Produces(HttpContentType.Json)]
        [ProducesResponseType(typeof(RuleResponse), 200)]
        public async Task<IActionResult> Post([FromBody] RuleRequest request)
        {
            var runner = new RuleRunner(new RuleDefinitionRepository(_context));
            var result = await runner.ApplyAsync(request);
            return Ok(result);
        }
    }
}
