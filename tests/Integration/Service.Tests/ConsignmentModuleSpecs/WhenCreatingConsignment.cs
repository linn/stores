namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingConsignment : ContextBase
    {
        private ConsignmentResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ConsignmentResource { SalesAccountId = 1 };
            this.ConsignmentFacadeService.Add(Arg.Any<ConsignmentResource>())
                .Returns(new SuccessResult<Consignment>(new Consignment { SalesAccountId = 1 }));

            this.Response = this.Browser.Post(
                "logistics/consignments",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ConsignmentFacadeService
                .Received()
                .Add(Arg.Is<ConsignmentResource>(r => r.SalesAccountId == this.requestResource.SalesAccountId));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ConsignmentResource>();
            resource.SalesAccountId.Should().Be(this.requestResource.SalesAccountId);
        }
    }
}
