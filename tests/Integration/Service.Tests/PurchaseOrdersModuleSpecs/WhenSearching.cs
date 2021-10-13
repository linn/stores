namespace Linn.Stores.Service.Tests.PurchaseOrdersModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var purchaseOrders = new List<PurchaseOrder>
                                     {
                                         new PurchaseOrder
                                             {
                                                 OrderNumber = 11818218,
                                                 SupplierId = 12345,
                                                 DocumentType = "no idea",
                                                 Details = new List<PurchaseOrderDetail>
                                                               {
                                                                   new PurchaseOrderDetail
                                                                       {
                                                                           Line = 1,
                                                                           OrderNumber = 11818218,
                                                                           SuppliersDesignation =
                                                                               "designation description",
                                                                           SalesArticle = new SalesArticle
                                                                               {
                                                                                   ArticleNumber = "vingt deux",
                                                                                   Description =
                                                                                       "the quick broon fox",
                                                                                   TariffId = 1234,
                                                                                   Tariff = new Tariff
                                                                                       {
                                                                                           TariffId = 1234,
                                                                                           TariffCode =
                                                                                               "845512340"
                                                                                       }
                                                                               }
                                                                       }
                                                               }
                                             },
                                         new PurchaseOrder
                                             {
                                                 OrderNumber = 11111,
                                                 SupplierId = 22345,
                                                 DocumentType = "no idea 2",
                                                 Details = new List<PurchaseOrderDetail>
                                                               {
                                                                   new PurchaseOrderDetail
                                                                       {
                                                                           Line = 1,
                                                                           OrderNumber = 11818218,
                                                                           SuppliersDesignation =
                                                                               "designation description",
                                                                           SalesArticle = new SalesArticle
                                                                               {
                                                                                   ArticleNumber = "vingt deux",
                                                                                   Description =
                                                                                       "the quick broon fox",
                                                                                   TariffId = 1234,
                                                                                   Tariff = new Tariff
                                                                                       {
                                                                                           TariffId = 1234,
                                                                                           TariffCode =
                                                                                               "845512340"
                                                                                       }
                                                                               }
                                                                       }
                                                               }
                                             }
                                     };

            this.PurchaseOrderFacadeService.Search(Arg.Any<string>())
                .Returns(new SuccessResult<IEnumerable<PurchaseOrder>>(purchaseOrders));

            this.Response = this.Browser.Get(
                "/logistics/purchase-orders",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "11");
                    }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.PurchaseOrderFacadeService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<PurchaseOrderResource>>();
            resources.Count().Should().Be(2);
        }
    }
}
