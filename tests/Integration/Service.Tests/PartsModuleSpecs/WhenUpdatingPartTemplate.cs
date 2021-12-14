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

    public class WhenUpdatingPartTemplate : ContextBase
    {
        private readonly string partId = "LRPT";

        [SetUp]
        public void SetUp()
        {
            var privileges = new List<string> { "part.admin" };

            this.AuthorisationService.HasPermissionFor("part.admin", privileges).Returns(true);

            var partTemplate = new PartTemplate
                                   {
                                       PartRoot = this.partId,
                                       Description = "A test for updating a part template",
                                       HasDataSheet = "N",
                                       HasNumberSequence = "Y",
                                       NextNumber = 32,
                                       AllowVariants = "Y",
                                       Variants = "a part template being updated as part of a test.",
                                       AccountingCompany = "LINN RECORDS",
                                       ProductCode = "LINNPT",
                                       StockControlled = "Y",
                                       LinnProduced = "Y",
                                       RmFg = "L",
                                       BomType = "A",
                                       AssemblyTechnology = "RS",
                                       AllowPartCreation = "Y",
                                       ParetoCode = "J"
                                   };

            var partTemplateResource = new PartTemplateResource
                                           {
                                               Description = "A test for updating a part template",
                                               HasDataSheet = "N",
                                               HasNumberSequence = "Y",
                                               NextNumber = 32,
                                               AllowVariants = "Y",
                                               Variants = "a part template being updated as part of a test.",
                                               AccountingCompany = "LINN RECORDS",
                                               ProductCode = "LINNPT",
                                               StockControlled = "Y",
                                               LinnProduced = "Y",
                                               RmFg = "L",
                                               BomType = "A",
                                               AssemblyTechnology = "RS",
                                               AllowPartCreation = "Y",
                                               ParetoCode = "J"
                                           };

            this.PartTemplateService.Update(partTemplate.PartRoot, Arg.Any<PartTemplateResource>(), Arg.Any<IEnumerable<string>>())
                .Returns(new SuccessResult<ResponseModel<PartTemplate>>(new ResponseModel<PartTemplate>(partTemplate, privileges)));

            this.Response = this.Browser.Put(
                "/inventory/part-templates/LRPT",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(partTemplateResource);
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
            this.PartTemplateService.Received().Update(this.partId, Arg.Any<PartTemplateResource>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartTemplateResource>();
            resource.Description.Should().Be("A test for updating a part template");
            resource.NextNumber.Should().Be(32);
        }
    }
}
