using Hein.RulesEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Application.Engine
{
    public interface IRuleRunner : IDisposable
    {
        void ApplyRules(IDictionary<string, object> parameters);
        RuleResult Result { get; }
    }

    public class RuleRunner : IRuleRunner
    {
        private readonly Entity _entity;
        private readonly IEnumerable<Rule> _rules;
        public RuleRunner(RuleDefinition def)
        {
            _entity = def.Entity;
            var rules = def.Rules.OrderBy(x => x.Priority).ToList();

            var defaultRule = def.Default;
            if (defaultRule != null)
            {
                defaultRule.Priority = int.MaxValue;
                rules.Add(def.Default);
            }

            _rules = rules;
        }

        public RuleResult Result { get; private set; }

        public void ApplyRules(IDictionary<string, object> parameters)
        {
            var tasks = new List<Task>();
            var results = new List<RuleTracker>();
            var props = _entity.Properties;

            foreach (var rule in _rules)
            {
                tasks.Add(Task.Run(() =>
                {
                    var executor = new RuleExecutor(props, parameters);
                    var result = executor.Run(rule);
                    results.Add(result);

                    return;
                }));
            }
            Task.WaitAll(tasks.ToArray());

            var passedRule = results
                .Where(x => x.Passed)
                .OrderBy(x => x.Priority)
                .FirstOrDefault();

            if (passedRule != null)
            {
                Result = new RuleResult()
                {
                    RuleName = passedRule.Name,
                    Results = passedRule.Results
                };
            }
            else
            {
                Result = new RuleResult()
                {
                    RuleName = "no_successful_rule"
                };
            }
        }


        public void Dispose()
        {
            GC.Collect();
        }
    }
}
