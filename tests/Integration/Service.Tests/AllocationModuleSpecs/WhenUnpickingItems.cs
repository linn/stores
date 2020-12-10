namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnpickingItems : ContextBase
    {
        private AccountOutletRequestResource resource;

        private List<SosAllocDetail> result;

        [SetUp]
        public void SetUp()
        {
            this.result = new List<SosAllocDetail> { new SosAllocDetail { Id = 1 } };
            this.resource = new AccountOutletRequestResource { JobId = 808, AccountId = 21, OutletNumber = 8 };
            this.AllocationFacadeService.UnpickItems(Arg.Is<AccountOutletRequestResource>(
                    a => a.AccountId == this.resource.AccountId
                         && a.OutletNumber == this.resource.OutletNumber
                         && a.JobId == this.resource.JobId))
                .Returns(new SuccessResult<IEnumerable<SosAllocDetail>>(this.result));

            this.Response = this.Browser.Post(
                $"/logistics/allocations/unpick",
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
                .UnpickItems(
                    Arg.Is<AccountOutletRequestResource>(
                        a => a.AccountId == this.resource.AccountId
                                                                    && a.OutletNumber == this.resource.OutletNumber
                                                                    && a.JobId == this.resource.JobId));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<SosAllocDetailResource>>();
            resultResource.First().Id.Should().Be(this.result.First().Id);
        }
    }
}
