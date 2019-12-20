using Hein.RulesEngine.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Domain.Repositories
{
    public interface IRuleDefinitonRepository
    {
        IEnumerable<RuleDefinition> GetDefinitons();
        Task<IEnumerable<RuleDefinition>> GetDefinitionsAsync();
        RuleDefinition GetDefinitionById(string id);
        Task<RuleDefinition> GetDefinitionByIdAsync(string id);
        RuleDefinition GetDefinitionByName(string name);
        Task<RuleDefinition> GetDefinitionByNameAsync(string name);
        IEnumerable<Rule> GetRulesByDefinitionName(string name);
        Task<IEnumerable<Rule>> GetRulesByDefinitionNameAsync(string groupName);

        RuleDefinition Save(RuleDefinition definition);
        Task<RuleDefinition> SaveAsync(RuleDefinition definition);
    }

    public class RuleDefinitionRepository : IRuleDefinitonRepository
    {

    }
}
