using Amazon.DynamoDBv2;
using Hein.Framework.Dynamo;
using Hein.RulesEngine.Application.Models;
using Hein.RulesEngine.Domain.Models;
using Hein.RulesEngine.Web.Controllers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Hein.RulesEngine.Tests.Web.Controllers
{
    public class ExecuteControllerTests
    {
        [Fact]
        public void Should_apply_mocked_repository_rules_cleanly()
        {
            var sampleDefinition = new RuleDefinition()
            {
                Name = "event_id_is_status",
                Properties = new List<EntityProperty>()
                {
                    new EntityProperty() { Name = "EventId", Type = "integer" },
                },
                Rules = new List<Rule>()
                {
                    new Rule()
                    {
                        IsEnabled = true,
                        Name = "fresh_status",
                        Priority = 1,
                        Conditions = new List<RuleParameters>()
                        {
                            new RuleParameters() { Property = "EventId", Operator = "IsOneOf", Value = "3005,3010" }
                        },
                        Results = new List<EntityPropertyResult>()
                        {
                            new EntityPropertyResult() { Name = "IsStatus", Value = "true", Type = "boolean" }
                        }
                    }
                }
            };

            var mockedResults = new List<RuleDefinition>()
            {
                sampleDefinition
            };

            var mockedContext = new Mock<IRepositoryContext>();
            mockedContext.Setup(x => x.Query<RuleDefinition>(It.IsAny<AmazonDynamoDBRequest>())).Returns(mockedResults);

            var controller = new ExecuteController(mockedContext.Object);

            var result = controller.Post(new RuleRequest()
            {
                Rule = "event_id_is_status",
                Parameters = new Dictionary<string, string>()
                {
                    { "EventId", "3005" }
                }
            }).Result;
        }
    }
}
