namespace Linn.Stores.Service.Tests.StoragePlaceAuditModuleSpecs
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

    public class WhenGettingAuditLocations : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a1 = new AuditLocation { StoragePlace = "p1" };
            var a2 = new AuditLocation { StoragePlace = "p2" };

            this.AuditLocationService.GetAuditLocations("p").Returns(
                new SuccessResult<IEnumerable<AuditLocation>>(new List<AuditLocation> { a1, a2 }));

            this.Response = this.Browser.Get(
                "/inventory/audit-locations",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "p");
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
            this.AuditLocationService.Received().GetAuditLocations("p");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<AuditLocationResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.StoragePlace == "p1");
            resource.Should().Contain(a => a.StoragePlace == "p2");
        }
    }
}