namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSourcesReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new MechPartSource
            {
                PartNumber = "PART/A",
            };
            var b = new MechPartSource
            {
                PartNumber = "PART/B",
            };

            this.MechPartSourceService.FilterBy(Arg.Is<MechPartSourceSearchResource>(
                    x => x.Description == "DESCRIPTION" && x.PartNumber == "PART" && x.ProjectDeptCode == "1234"))
                .Returns(new SuccessResult<IEnumerable<MechPartSource>>(new List<MechPartSource> { a, b }));

            this.Response = this.Browser.Get(
                "/parts/sources/report",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("partNumber", "PART");
                    with.Query("description", "DESCRIPTION");
                    with.Query("projectDeptCode", "1234");
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
            this.MechPartSourceService.FilterBy(Arg.Is<MechPartSourceSearchResource>(
                x => x.Description == "DESCRIPTION" && x.PartNumber == "PART" && x.ProjectDeptCode == "1234"));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<MechPartSourceResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.PartNumber == "PART/A");
            resource.Should().Contain(a => a.PartNumber == "PART/B");
        }
    }
}
