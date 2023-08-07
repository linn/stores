namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingWithOrderDetails : ContextBase
    {
        private readonly int impbookId = 12007;
        private ImportBook impbook;

        [SetUp]
        public void SetUp()
        {
            var firstOrderDetail = new ImportBookOrderDetail
                                   {
                                       ImportBookId = this.impbookId,
                                       LineNumber = 1,
                                       OrderNumber = null,
                                       RsnNumber = null,
                                       OrderDescription = "kylo ren first order",
                                       Qty = 1,
                                       DutyValue = 21.12m,
                                       FreightValue = 22.12m,
                                       VatValue = 3.12m,
                                       OrderValue = 44.1m,
                                       Weight = 55.2m,
                                       LoanNumber = null,
                                       LineType = "typea",
                                       CpcNumber = null,
                                       TariffCode = "121213",
                                       InsNumber = null,
                                       VatRate = null
                                   };

            var secondOrderDetail = new ImportBookOrderDetail
                                    {
                                        ImportBookId = this.impbookId,
                                        LineNumber = 2,
                                        OrderNumber = 13,
                                        RsnNumber = 2,
                                        OrderDescription = "palpatine final order",
                                        Qty = 1,
                                        DutyValue = 21.12m,
                                        FreightValue = 22.12m,
                                        VatValue = 3.12m,
                                        OrderValue = 44.1m,
                                        Weight = 55.2m,
                                        LoanNumber = null,
                                        LineType = "TYpe B",
                                        CpcNumber = null,
                                        TariffCode = "121213",
                                        InsNumber = null,
                                        VatRate = null
                                    };

            var updatedFirstOrderDetail = new ImportBookOrderDetail
                                          {
                                              ImportBookId = this.impbookId,
                                              LineNumber = 1,
                                              OrderNumber = 111,
                                              RsnNumber = 222,
                                              OrderDescription = "kylo ren first order",
                                              Qty = 3,
                                              DutyValue = 91.12m,
                                              FreightValue = 92.12m,
                                              VatValue = 93.12m,
                                              OrderValue = 944.1m,
                                              Weight = 955.2m,
                                              LoanNumber = 999,
                                              LineType = "Type C",
                                              CpcNumber = 91,
                                              TariffCode = "121213",
                                              InsNumber = 92,
                                              VatRate = 93
                                          };

            this.impbook = new ImportBook
                           {
                               Id = this.impbookId,
                               DateCreated = DateTime.Now.AddDays(-5),
                               SupplierId = 555,
                               CarrierId = 678,
                               TransportId = 1,
                               TransactionId = 44,
                               TotalImportValue = 123.4m,
                               InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                               OrderDetails = new List<ImportBookOrderDetail> { firstOrderDetail },
                               PostEntries = new List<ImportBookPostEntry>()
                           };

            var newImportBook = new ImportBook
                                {
                                    Id = this.impbookId,
                                    DateCreated = DateTime.Now.AddDays(-5),
                                    SupplierId = 555,
                                    CarrierId = 678,
                                    TransactionId = 44,
                                    TotalImportValue = 123.4m,
                                    InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                                    OrderDetails =
                                        new List<ImportBookOrderDetail> { updatedFirstOrderDetail, secondOrderDetail },
                                    PostEntries = new List<ImportBookPostEntry>()
                                };

            this.LedgerPeriodRepository.FindBy(Arg.Any<Expression<Func<LedgerPeriod, bool>>>())
                .Returns(new LedgerPeriod { PeriodNumber = 1234 });

            this.Sut.Update(this.impbook, newImportBook);
        }

        [Test]
        public void ShouldHaveUpdatedInvoiceDetail()
        {
            this.impbook.OrderDetails.FirstOrDefault(
                x => x.ImportBookId == this.impbookId && x.LineNumber == 1 && x.OrderNumber == 111 && x.RsnNumber == 222
                     && x.OrderDescription == "kylo ren first order" && x.Qty == 3 && x.DutyValue == 91.12m
                     && x.FreightValue == 92.12m && x.VatValue == 93.12m && x.OrderValue == 944.1m && x.Weight == 955.2m
                     && x.LoanNumber == 999 && x.LineType == "Type C" && x.CpcNumber == 91 && x.TariffCode == "121213"
                     && x.InsNumber == 92 && x.VatRate == 93).Should().NotBeNull();
        }

        [Test]
        public void ShouldHaveAddedInvoiceDetail()
        {
            this.impbook.OrderDetails.Count().Should().Be(2);
            this.impbook.OrderDetails.FirstOrDefault(
                x => x.ImportBookId == this.impbookId && x.LineNumber == 2 && x.OrderNumber == 13 && x.RsnNumber == 2
                     && x.OrderDescription == "palpatine final order" && x.Qty == 1 && x.DutyValue == 21.12m
                     && x.FreightValue == 22.12m && x.VatValue == 3.12m && x.OrderValue == 44.1m && x.Weight == 55.2m
                     && x.LoanNumber == null && x.LineType == "TYpe B" && x.CpcNumber == null
                     && x.TariffCode == "121213" && x.InsNumber == null && x.VatRate == null).Should().NotBeNull();
        }
    }
}
