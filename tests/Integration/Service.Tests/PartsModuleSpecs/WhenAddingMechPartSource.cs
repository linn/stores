namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingMechPartSource : ContextBase
    {
        private MechPartSourceResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new MechPartSourceResource
            {
                Id = 1,
                ProposedBy = 33087,
                AssemblyType = "SM",
                SamplesRequired = "N"
            };

                var source = new MechPartSource
                {
                    Id = 1,
                    ProposedBy = new Employee { Id = 33087 },
                    AssemblyType = "SM",
                    SamplesRequired = "N"
                };

            this.MechPartSourceService.Add(Arg.Any<MechPartSourceResource>())
                .Returns(new CreatedResult<MechPartSource>(source));

            this.Response = this.Browser.Post(
                "/parts/sources",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.MechPartSourceService
                .Received()
                .Add(Arg.Is<MechPartSourceResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<MechPartSource>();
            resource.Id.Should().Be(1);
        }
    }
}
