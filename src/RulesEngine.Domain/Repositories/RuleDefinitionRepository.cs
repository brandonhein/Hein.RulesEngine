using Hein.Framework.Dynamo;
using Hein.Framework.Dynamo.Criterion;
using Hein.Framework.Dynamo.Entity;
using Hein.RulesEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<IEnumerable<Rule>> GetRulesByDefinitionNameAsync(string name);

        RuleDefinition Save(RuleDefinition definition);
        Task<RuleDefinition> SaveAsync(RuleDefinition definition);
    }

    public class RuleDefinitionRepository : IRuleDefinitonRepository
    {
        private readonly EntityRepository<RuleDefinition> _entityRepository;
        public RuleDefinitionRepository(IRepositoryContext context)
        {
            _entityRepository = new EntityRepository<RuleDefinition>(context);
        }

        public RuleDefinition GetDefinitionById(string id)
        {
            return GetDefinitionByIdAsync(id).Result;
        }

        public Task<RuleDefinition> GetDefinitionByIdAsync(string id)
        {
            if (Guid.TryParse(id, out var guidfied))
            {
                var query = QueryOver.Of<RuleDefinition>()
                    .Where(x => x.DefintionId == guidfied)
                    .Top(1);

                var results = _entityRepository.Find(query);
                return Task.FromResult(results.FirstOrDefault(x => x.DefintionId == guidfied));
            }

            throw new KeyNotFoundException();
        }

        public RuleDefinition GetDefinitionByName(string name)
        {
            return GetDefinitionByNameAsync(name).Result;
        }

        public Task<RuleDefinition> GetDefinitionByNameAsync(string name)
        {
            var query = QueryOver.Of<RuleDefinition>()
                .Where(x => x.Name == name)
                .Top(1);

            var results = _entityRepository.Find(query);
            return Task.FromResult(results.FirstOrDefault(x => x.Name == name));
        }

        public Task<IEnumerable<RuleDefinition>> GetDefinitionsAsync()
        {
            return Task.FromResult(_entityRepository.GetAll());
        }

        public IEnumerable<RuleDefinition> GetDefinitons()
        {
            return GetDefinitionsAsync().Result;
        }

        public IEnumerable<Rule> GetRulesByDefinitionName(string name)
        {
            var definition = GetDefinitionByName(name);
            return definition.Rules.Where(x => x.IsEnabled);
        }

        public async Task<IEnumerable<Rule>> GetRulesByDefinitionNameAsync(string name)
        {
            var defintion = await GetDefinitionByNameAsync(name);
            return defintion.Rules.Where(x => x.IsEnabled);
        }

        public RuleDefinition Save(RuleDefinition definition)
        {
            return SaveAsync(definition).Result;
        }

        public Task<RuleDefinition> SaveAsync(RuleDefinition definition)
        {
            _entityRepository.Save(definition);
            return Task.FromResult(definition);
        }
    }
}
