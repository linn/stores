namespace Linn.Stores.Service.Tests.RsnModuleSpecs
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

    public class WhenGettingRsns : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var rsn1 = new Rsn { RsnNumber = 1, AccountId = 123, OutletNumber = 1 };
            var rsn2 = new Rsn { RsnNumber = 2, AccountId = 123, OutletNumber = 1 };

            this.RsnService.SearchRsns(123, 1)
                .Returns(new SuccessResult<IEnumerable<Rsn>>(new List<Rsn> { rsn1, rsn2 }));

            this.Response = this.Browser.Get(
                "/inventory/rsns",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("accountId", "123");
                        with.Query("outletNumber", "1");
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
            this.RsnService.Received().SearchRsns(123, 1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<RsnResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(r => r.RsnNumber == 1);
            resource.Should().Contain(r => r.RsnNumber == 2);
        }
    }
}