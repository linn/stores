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

    public class WhenGettingPartTemplates : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new PartTemplate
                        {
                            PartRoot = "PART A"
                        };

            var b = new PartTemplate
                           {
                               PartRoot = "PART B"
                           };
            
            this.partTemplateService.GetAll()
                .Returns(new SuccessResult<IEnumerable<PartTemplate>>(new List<PartTemplate> { a, b }));


            this.Response = this.Browser.Get(
                "/inventory/part-templates",
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
            this.partTemplateService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartTemplateResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.PartRoot == "PART A");
        }
    }
}