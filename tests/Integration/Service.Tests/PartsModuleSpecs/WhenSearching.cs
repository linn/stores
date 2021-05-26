namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var partA = new Part
                            {
                                PartNumber = "PART/A",
                                Description = "description A",
                                StockControlled = "Y",
                                CreatedBy = new Employee { Id = 1 }
                            };
            var partB = new Part
                            {
                                PartNumber = "PART/B",
                                Description = "description B",
                                StockControlled = "Y",
                                CreatedBy = new Employee { Id = 1 }
                            };

            this.PartsFacadeService.Search("P")
                .Returns(new SuccessResult<IEnumerable<Part>>(new List<Part> { partA, partB }));

            this.Response = this.Browser.Get(
                "/parts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "P");
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
            this.PartsFacadeService.Received().Search("P");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.PartNumber == "PART/A");
            resource.Should().Contain(a => a.PartNumber == "PART/B");
        }
    }
}