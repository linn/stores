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

    public class WhenGettingSalesOutletAddresses : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var outlet1 = new SalesOutlet { AccountId = 1, OutletNumber = 1, Name = "hifi studio", OutletAddressId = 10 };
            var outlet2 = new SalesOutlet { AccountId = 1, OutletNumber = 2, Name = "spoons are us", OutletAddressId = 11 };

            this.SalesOutletService.GetOutletAddresses(1,"hifi").Returns(
                new SuccessResult<IEnumerable<SalesOutlet>>(new List<SalesOutlet> { outlet1 }));

            this.Response = this.Browser.Get(
                "/inventory/sales-outlets/addresses",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("accountId", "1");
                    with.Query("searchTerm", "hifi");
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
            this.SalesOutletService.Received().GetOutletAddresses(1, "hifi");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<SalesOutletResource>>().ToList();
            resource.Should().HaveCount(1);
            resource.Should().Contain(s => s.Name == "hifi studio");
        }
    }
}