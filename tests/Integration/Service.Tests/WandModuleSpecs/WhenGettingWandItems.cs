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

    public class WhenGettingWandItems : ContextBase
    {
        private IEnumerable<WandItem> items;

        private int consignmentId;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 345;
            this.items = new List<WandItem>
                                    {
                                        new WandItem { PartNumber = "pn1" },
                                        new WandItem { PartNumber = "pn2" }
                                    };
            this.WandFacadeService.GetWandItems(this.consignmentId)
                .Returns(new SuccessResult<IEnumerable<WandItem>>(this.items));

            this.Response = this.Browser.Get(
                $"/logistics/wand/items",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.consignmentId.ToString());
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
            this.WandFacadeService.Received().GetWandItems(this.consignmentId);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResources = this.Response.Body.DeserializeJson<IEnumerable<WandItemResource>>().ToList();
            resultResources.Should().HaveCount(2);
            resultResources.Should().Contain(a => a.PartNumber == "pn1");
            resultResources.Should().Contain(a => a.PartNumber == "pn1");
        }
    }
}
