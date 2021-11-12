namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

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
        [SetUp]
        public void SetUp()
        {
            var a = new PartTemplate
                        {
                            PartRoot = "PARTTEMP"
                        };

            this.PartTemplateService.GetById(a.PartRoot)
                .Returns(new SuccessResult<PartTemplate>(a));

            this.Response = this.Browser.Get(
                "/inventory/part-templates/PARTTEMP",
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
            this.PartTemplateService.Received().GetById("PARTTEMP");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartTemplateResource>();
            resource.PartRoot.Should().Be("PARTTEMP");
        }
    }
}