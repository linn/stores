namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocation : ContextBase
    {
        private AllocationOptionsResource resource;

        private AllocationResult allocationStartDetails;

        [SetUp]
        public void SetUp()
        {
            this.allocationStartDetails = new AllocationResult(2934762);

            this.resource = new AllocationOptionsResource
                                {
                                    AccountId = 111,
                                    ArticleNumber = "article",
                                    StockPoolCode = "st",
                                    DespatchLocationCode = "d",
                                    ExcludeOnHold = true,
                                    ExcludeOverCreditLimit = true,
                                    ExcludeUnsuppliableLines = true
                                };

            this.AllocationFacadeService.StartAllocation(Arg.Any<AllocationOptionsResource>())
                .Returns(new SuccessResult<AllocationResult>(this.allocationStartDetails));

            this.Response = this.Browser.Post(
                "/logistics/allocations",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.resource);
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
            this.AllocationFacadeService.Received()
                .StartAllocation(Arg.Is<AllocationOptionsResource>(
                        r => r.StockPoolCode == this.resource.StockPoolCode
                             && r.ArticleNumber == this.resource.ArticleNumber
                             && r.AccountId == this.resource.AccountId
                             && r.DespatchLocationCode == this.resource.DespatchLocationCode
                             && r.ExcludeOnHold == this.resource.ExcludeOnHold
                             && r.ExcludeOverCreditLimit == this.resource.ExcludeOverCreditLimit
                             && r.ExcludeUnsuppliableLines == this.resource.ExcludeUnsuppliableLines));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<AllocationResource>();
            resultResource.Id.Should().Be(this.allocationStartDetails.Id);
            resultResource.Links.First(a => a.Rel == "display-results").Href.Should()
                .Be($"/logistics/sos-alloc-heads/{this.allocationStartDetails.Id}");
        }
    }
}
