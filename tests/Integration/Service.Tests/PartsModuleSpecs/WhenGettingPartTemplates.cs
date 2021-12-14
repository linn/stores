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
        private ResponseModel<PartTemplate> partTemplateResponseModel;

        private PartTemplate partTemplate;

        [SetUp]
        public void SetUp()
        {
            var privileges = new List<string> { "part.admin" };

            this.AuthorisationService.HasPermissionFor("part.admin", privileges).Returns(true);

            this.partTemplate = new PartTemplate 
                                    {
                                        PartRoot = "PART A"
                                    };

            this.partTemplateResponseModel = new ResponseModel<PartTemplate>(
                this.partTemplate,
                new List<string>());

            this.PartTemplateService.GetAll(Arg.Any<IEnumerable<string>>())
                .Returns(new SuccessResult<ResponseModel<IEnumerable<PartTemplate>>>(new ResponseModel<IEnumerable<PartTemplate>>(new List<PartTemplate> { this.partTemplate }, privileges)));


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
            this.PartTemplateService.Received().GetAll(Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartTemplateResource>>().ToList();
            resource.First().Description.Should().Be("A test for updating a part template");
            resource.First().NextNumber.Should().Be(32);
        }
    }
}
