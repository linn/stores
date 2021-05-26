namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDecrementRules : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new DecrementRule
                        {
                            Rule = "A"
                        };
            var b = new DecrementRule
                        {
                            Rule = "B"
                        };

            this.DecrementRuleService.GetAll()
                .Returns(new SuccessResult<IEnumerable<DecrementRule>>(
                    new List<DecrementRule> { a, b }));

            this.Response = this.Browser.Get(
                "/inventory/decrement-rules",
                with =>
                    {
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.DecrementRuleService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<DecrementRuleResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Rule == "A");
            resource.Should().Contain(a => a.Rule == "B");
        }
    }
}