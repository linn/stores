namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSosAllocDetail : ContextBase
    {
        private SosAllocDetail sosAllocDetail;

        private SosAllocDetailResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new SosAllocDetailResource { QuantityToAllocate = 222 };
            this.sosAllocDetail = new SosAllocDetail { Id = 11, AccountId = 1, JobId = 808, QuantityToAllocate = 222 };

            this.SosAllocDetailFacadeService
                .Update(11, Arg.Is<SosAllocDetailResource>(a => a.QuantityToAllocate == 222))
                .Returns(new SuccessResult<SosAllocDetail>(this.sosAllocDetail));

            this.Response = this.Browser.Put(
                "/logistics/sos-alloc-details/11",
                with =>
                {
                    with.Header("Accept", "application/json");
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
            this.SosAllocDetailFacadeService
                .Received()
                .Update(11, Arg.Is<SosAllocDetailResource>(a => a.QuantityToAllocate == 222));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<SosAllocDetailResource>();
            resultResource.Id.Should().Be(11);
        }
    }
}
