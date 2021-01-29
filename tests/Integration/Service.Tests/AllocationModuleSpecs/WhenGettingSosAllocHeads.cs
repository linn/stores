namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSosAllocHeads : ContextBase
    {
        private SosAllocHead sosAllocHead1;

        private SosAllocHead sosAllocHead2;

        [SetUp]
        public void SetUp()
        {
            this.sosAllocHead1 = new SosAllocHead
                                     {
                                         AccountId = 1,
                                         OutletNumber = 1,
                                         JobId = 222,
                                         SalesOutlet = new SalesOutlet
                                                           {
                                                               AccountId = 1,
                                                               OutletNumber = 1,
                                                               Name = "dom",
                                                               CountryCode = "GB",
                                                               CountryName = "UK"
                                                           }
                                     };
            this.sosAllocHead2 = new SosAllocHead
                                     {
                                         AccountId = 2,
                                         JobId = 222,
                                         SalesOutlet = new SalesOutlet { AccountId = 2, OutletNumber = 2, Name = "two" }
                                     };

            this.SosAllocHeadFacadeService.GetAllocHeads(222)
                .Returns(new SuccessResult<IEnumerable<SosAllocHead>>(new List<SosAllocHead>
                                                                              {
                                                                                  this.sosAllocHead1, this.sosAllocHead2
                                                                              }));

            this.Response = this.Browser.Get(
                "/logistics/sos-alloc-heads/222",
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
            this.SosAllocHeadFacadeService.Received().GetAllocHeads(222);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<SosAllocHeadResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.First(a => a.AccountId == 1).JobId.Should().Be(222);
            resultResource.First(a => a.AccountId == 1).OutletName.Should().Be(this.sosAllocHead1.SalesOutlet.Name);
            resultResource.First(a => a.AccountId == 1).CountryCode.Should().Be(this.sosAllocHead1.SalesOutlet.CountryCode);
            resultResource.First(a => a.AccountId == 1).CountryName.Should().Be(this.sosAllocHead1.SalesOutlet.CountryName);
            resultResource.First(a => a.AccountId == 2).JobId.Should().Be(222);
        }
    }
}
