namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;

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
            var privileges = new List<string> { "part.admin" };

            this.PartTemplateService.GetById(partTemp.PartRoot, Arg.Any<IEnumerable<string>>()).Returns(new SuccessResult<ResponseModel<PartTemplate>>(new ResponseModel<PartTemplate>(partTemp, privileges)));


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
            this.PartTemplateService.Received().GetById(Arg.Any<string>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartTemplateResource>();
            resource.PartRoot.Should().Be(this.partRootString);
        }
    }
}
