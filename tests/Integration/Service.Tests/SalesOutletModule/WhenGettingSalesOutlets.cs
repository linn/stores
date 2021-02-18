namespace Linn.Stores.Service.Tests.SalesOutletModule
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

    public class WhenGettingSalesOutlets : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var outlet1 = new SalesOutlet { AccountId = 1, OutletNumber = 1, Name = "so1" };
            var outlet2 = new SalesOutlet { AccountId = 2, OutletNumber = 2, Name = "so2" };

            this.SalesOutletService.GetSalesOutlets("so").Returns(
                new SuccessResult<IEnumerable<SalesOutlet>>(new List<SalesOutlet> { outlet1, outlet2 }));

            this.Response = this.Browser.Get(
                "/inventory/sales-outlets",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "so");
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
            this.SalesOutletService.Received().GetSalesOutlets("so");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<SalesOutletResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(s => s.Name == "so1");
            resource.Should().Contain(s => s.Name == "so2");
        }
    }
}