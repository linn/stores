namespace Linn.Stores.Service.Tests.TpkModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTransferableStockList : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var transferableStockA = new TransferableStock
                                {
                                    ArticleNumber = "A"
                                };
            var transferableStockB = new TransferableStock
                                {
                                    ArticleNumber = "B"
                                };

            this.TpkFacadeService.GetTransferableStock()
                .Returns(new SuccessResult<IEnumerable<TransferableStock>>(new List<TransferableStock> { transferableStockA, transferableStockB }));


            this.Response = this.Browser.Get(
                "/logistics/tpk/items",
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
            this.TpkFacadeService.Received().GetTransferableStock();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<TransferableStockResource>>().ToList();
            resource.Should().HaveCount(2);
        }
    }
}
