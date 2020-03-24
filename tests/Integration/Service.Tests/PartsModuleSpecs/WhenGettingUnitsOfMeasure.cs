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

    public class WhenGettingUnitsOfMeasure : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var unitA = new UnitOfMeasure
                                   {
                                       Unit = "ONES"
                                   };
            var unitB = new UnitOfMeasure
                                   {
                                       Unit = "TWOS"
                                   };

            this.UnitsOfMeasureService.GetUnitsOfMeasure()
                .Returns(new SuccessResult<IEnumerable<UnitOfMeasure>>(new List<UnitOfMeasure> { unitA, unitB }));


            this.Response = this.Browser.Get(
                "/inventory/units-of-measure",
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
            this.UnitsOfMeasureService.Received().GetUnitsOfMeasure();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<UnitOfMeasureResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Unit == "ONES");
            resource.Should().Contain(a => a.Unit == "TWOS");
        }
    }
}