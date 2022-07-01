namespace Linn.Stores.Service.Tests.TqmsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Resources.Tqms;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTqmsCategories : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            IEnumerable<TqmsCategory> results = new List<TqmsCategory>
                                                    {
                                                        new TqmsCategory { Category = "C1" },
                                                        new TqmsCategory { Category = "C2" }
                                                    };
            this.TqmsCategoryFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<TqmsCategory>>(results));

            this.Response = this.Browser.Get(
                "/inventory/tqms-categories",
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
            this.TqmsCategoryFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<TqmsCategoryResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Category == "C1");
            resource.Should().Contain(a => a.Category == "C2");
        }
    }
}
