namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingPartFromSource : ContextBase
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
                SamplesRequired = "N",
                CreatePart = true
            };

            var source = new MechPartSource
                             {
                                 Id = 1,
                                 ProposedBy = new Employee { Id = 33087 },
                                 AssemblyType = "SM",
                                 SamplesRequired = "N"
                             };

            var part = new Part
                           {
                               Id = 1,
                               StockControlled = "Y",
                               CreatedBy = new Employee { Id = 33087 },
                           };

            this.MechPartSourceService.Add(Arg.Any<MechPartSourceResource>())
                .Returns(new CreatedResult<MechPartSource>(source));
            this.PartsDomainService.CreateFromSource(1, 33087, new List<PartDataSheet>()).Returns(part);
            this.MechPartSourceService.GetById(1).Returns(new SuccessResult<MechPartSource>(source));
            this.AuthService.HasPermissionFor(
                Arg.Any<string>(),
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Post(
                "/inventory/parts/sources",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldAddMechPartSource()
        {
            this.MechPartSourceService
                .Received()
                .Add(Arg.Is<MechPartSourceResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldCreatePart()
        {
            this.PartsFacadeService.Received().CreatePartFromSource(1, 33087, null);
        }

        [Test]
        public void ShouldGetUpdatedSource()
        {
            this.MechPartSourceService.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<MechPartSource>();
            resource.Id.Should().Be(1);
        }
    }
}
