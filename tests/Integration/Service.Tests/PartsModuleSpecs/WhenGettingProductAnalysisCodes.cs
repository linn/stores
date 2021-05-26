namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingProductAnalysisCodes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var codeA = new ProductAnalysisCode
                           {
                               ProductCode = "A"
                           };
            var codeB = new ProductAnalysisCode
                           {
                               ProductCode = "B"
                           };

            this.ProductAnalysisCodeService.GetProductAnalysisCodes()
                .Returns(new SuccessResult<IEnumerable<ProductAnalysisCode>>(
                    new List<ProductAnalysisCode> { codeA, codeB }));

            this.Response = this.Browser.Get(
                "/inventory/product-analysis-codes",
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
            this.ProductAnalysisCodeService.Received().GetProductAnalysisCodes();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ProductAnalysisCodeResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.ProductCode == "A");
            resource.Should().Contain(a => a.ProductCode == "B");
        }
    }
}