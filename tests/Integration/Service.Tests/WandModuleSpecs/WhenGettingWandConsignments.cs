namespace Linn.Stores.Service.Tests.WandModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWandConsignments : ContextBase
    {
        private IEnumerable<WandConsignment> consignments;

        [SetUp]
        public void SetUp()
        {
            this.consignments = new List<WandConsignment>
                                    {
                                        new WandConsignment { ConsignmentId = 1 },
                                        new WandConsignment { ConsignmentId = 2 }
                                    };
            this.WandFacadeService.GetWandConsignments()
                .Returns(new SuccessResult<IEnumerable<WandConsignment>>(this.consignments));

            this.Response = this.Browser.Get(
                "/logistics/wand/consignments",
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
            this.WandFacadeService.Received().GetWandConsignments();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResources = this.Response.Body.DeserializeJson<IEnumerable<WandConsignmentResource>>().ToList();
            resultResources.Should().HaveCount(2);
            resultResources.Should().Contain(a => a.ConsignmentId == 1);
            resultResources.Should().Contain(a => a.ConsignmentId == 2);
        }
    }
}
