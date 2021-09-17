namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPackingList : ContextBase
    {
        private int consignmentId;

        private PackingList packingList;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 145;
            this.packingList = new PackingList { ConsignmentId = this.consignmentId };

            this.LogisticsReportsFacadeService.GetPackingList(this.consignmentId)
                .Returns(new SuccessResult<PackingList>(this.packingList));

            this.Response = this.Browser.Get(
                $"/logistics/consignments/{this.consignmentId}/packing-list",
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
            this.LogisticsReportsFacadeService.Received().GetPackingList(this.consignmentId);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<PackingListResource>();
            resultResource.ConsignmentId.Should().Be(this.consignmentId);
        }
    }
}
