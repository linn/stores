namespace Linn.Stores.Facade.Tests.PurchaseOrdersServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;

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

            this.PurchaseOrderRepository.FilterBy(Arg.Any<Expression<Func<PurchaseOrder, bool>>>())
                .Returns(purchaseOrders.AsQueryable());
        }

        [Test]
        public void ShouldCallSearch()
        {
            this.Sut.Search("111");
            this.PurchaseOrderRepository.Received().FilterBy(Arg.Any<Expression<Func<PurchaseOrder, bool>>>());
        }
        
        [Test]
        public void ShouldReturnSuccess()
        {
            var result = this.Sut.Search("111");
            result.Should().BeOfType<SuccessResult<IEnumerable<PurchaseOrder>>>();

            var dataResult = ((SuccessResult<IEnumerable<PurchaseOrder>>)result).Data;
            dataResult.FirstOrDefault(
                    x => x.OrderNumber == 11818218 && x.Details.Any(y => y.Line == 1)
                                                   && !x.Details.Any(z => z.Line == 2))
                .Should().NotBeNull();
            dataResult.FirstOrDefault(x => x.OrderNumber == 11111).Should().NotBeNull();
        }
    }
}
