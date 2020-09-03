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

    public class WhenGettingPartCategories : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var catA = new PartCategory
                            {
                                Category = "Cat A"
                            };
            var catB = new PartCategory
                            {
                                Category = "Cat B"
                            };

            this.PartCategoriesService.GetCategories()
                .Returns(new SuccessResult<IEnumerable<PartCategory>>(new List<PartCategory> { catA, catB }));


            this.Response = this.Browser.Get(
                "/inventory/part-categories",
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
            this.PartCategoriesService.Received().GetCategories();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartCategoryResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Category == "Cat A");
            resource.Should().Contain(a => a.Category == "Cat B");
        }
    }
}