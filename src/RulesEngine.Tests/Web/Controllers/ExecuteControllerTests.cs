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
                Name = "next_status_update",
                Properties = new List<EntityProperty>()
                {
                    new EntityProperty() { Name = "EventId", Type = "integer" },
                    new EntityProperty() { Name = "StatusId", Type = "integer" },
                    new EntityProperty() { Name = "IsCallEvent", Type = "boolean" },
                    new EntityProperty() { Name = "CallDuration", Type = "decimal" }
                },
                Rules = new List<Rule>()
                {
                    new Rule()
                    {
                        IsEnabled = true,
                        Name = "set_event_to_status",
                        Priority = 1,
                        Conditions = new List<RuleParameters>()
                        {
                            new RuleParameters() { Property = "EventId", Operator = "IsOneOf", Value = "1010,1020,1021,1100,1110,3020,3021,3025" }
                        },
                        Results = new List<EntityPropertyResult>()
                        {
                            new EntityPropertyResult() { Name = "StatusId", Value = "EventId", Type = "copy" }
                        }
                    },
                    new Rule()
                    {
                        IsEnabled = true,
                        Priority = 2,
                        Name = "incoming_call_not_long_enough_for_convo",
                        Conditions = new List<RuleParameters>()
                        {
                            new RuleParameters() { Property = "StatusId", Operator = "IsOneOf", Value ="3005,3010" },
                            new RuleParameters() { Property = "IsCallEvent", Operator = "==", Value = "true" },
                            new RuleParameters() { Property = "CallDuration", Operator = "<", Value = "127" }
                        },
                        Results = new List<EntityPropertyResult>()
                        {
                            new EntityPropertyResult() { Name = "StatusId", Value = "3010", Type = "integer" }
                        }
                    },
                    new Rule()
                    {
                        IsEnabled = true,
                        Priority = 3,
                        Name = "incoming_call_long_enough_for_convo",
                        Conditions = new List<RuleParameters>()
                        {
                            new RuleParameters() { Property = "StatusId", Operator = "IsOneOf", Value ="3005,3010" },
                            new RuleParameters() { Property = "IsCallEvent", Operator = "==", Value = "true" },
                            new RuleParameters() { Property = "CallDuration", Operator = ">=", Value = "127" }
                        },
                        Results = new List<EntityPropertyResult>()
                        {
                            new EntityPropertyResult() { Name = "StatusId", Value = "3015", Type = "integer" }
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
                Rule = "next_status_update",
                Parameters = new Dictionary<string, string>()
                {
                    { "EventId", "3020" },
                    { "StatusId", "3005" }
                }
            }).Result;
        }
    }
}
