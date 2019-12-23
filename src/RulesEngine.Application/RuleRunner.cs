using Hein.RulesEngine.Application.Models;
using Hein.RulesEngine.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Application
{
    public class RuleRunner
    {
        private readonly IRuleDefinitonRepository _repository;
        public RuleRunner(IRuleDefinitonRepository repository)
        {
            _repository = repository;
        }

        public RuleResponse Apply(RuleRequest request)
        {
            return ApplyAsync(request).Result;
        }

        public async Task<RuleResponse> ApplyAsync(RuleRequest request)
        {
            var definition = await _repository.GetDefinitionByNameAsync(request.Rule);
            var rules = definition.Rules?.OrderBy(x => x.Priority);

            if (rules == null || rules.Any())
            {
                return new RuleResponse()
                {
                    Rule = request.Rule,
                    Winner = "no_rule_found",
                    Values = null
                };
            }

            var properites = definition.Properties;
            var results = new List<RuleTracker>();
            var tasks = new List<Task>();

            foreach (var rule in rules)
            {
                tasks.Add(Task.Run(() =>
                {
                    var executor = new RuleExecutor(properites, request.Parameters);
                    var result = executor.Run(rule);
                    results.Add(result);
                }));
            }
            Task.WaitAll(tasks.ToArray());

            var passedRule = results
                .Where(x => x.Passed)
                .OrderBy(x => x.Priority)
                .FirstOrDefault();

            if (passedRule != null)
            {
                return new RuleResponse()
                {
                    Rule = request.Rule,
                    Winner = passedRule.Name,
                    Values = passedRule.Results
                };
            }
            else
            {
                return new RuleResponse()
                {
                    Rule = request.Rule,
                    Winner = "no_successful_rule",
                    Values = null
                };
            }
        }
    }
}
