namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByPartNumber : ContextBase
    {
        private string partNumber;

        private SearchRequestResource request;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "abc";
            this.request = new SearchRequestResource { ExactOnly = true, SearchTerm = this.partNumber };
            var p = new Part { Id = 1, PartNumber = this.partNumber, StockControlled = "Y", CreatedBy = new Employee { Id = 1 } };
            this.PartsFacadeService.GetPartByPartNumber(this.partNumber).Returns(new SuccessResult<IEnumerable<Part>>(new List<Part> { p }));

            this.Response = this.Browser.Get(
                "/parts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(this.request);
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
            this.PartsFacadeService.Received().GetPartByPartNumber(this.partNumber);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartResource>>().ToList();
            resource.First().Id.Should().Be(1);
            resource.First().PartNumber.Should().Be(this.partNumber);
        }
    }
}
