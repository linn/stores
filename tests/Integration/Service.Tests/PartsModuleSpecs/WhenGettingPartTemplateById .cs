namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPartTemplatesById : ContextBase
    {
        private readonly string partRootString = "PARTTEMP";

        [SetUp]
        public void SetUp()
        {
            var partTemp = new PartTemplate { PartRoot = this.partRootString };

            this.PartTemplateService.GetById(partTemp.PartRoot).Returns(new SuccessResult<PartTemplate>(partTemp));

            this.Response = this.Browser.Get(
                "/inventory/part-templates/PARTTEMP",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PartTemplateService.Received().GetById(this.partRootString);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartTemplateResource>();
            resource.PartRoot.Should().Be(this.partRootString);
        }
    }
}
