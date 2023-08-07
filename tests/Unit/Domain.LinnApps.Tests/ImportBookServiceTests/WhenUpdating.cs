namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private readonly int impbookId = 12007;

        private readonly DateTime now = DateTime.Now;

        private ImportBook impbook;

        private ImportBook newImpBook;

        [SetUp]
        public void SetUp()
        {
            this.impbook = new ImportBook
                           {
                               Id = this.impbookId,
                               DateCreated = this.now.AddDays(-5),
                               ParcelNumber = null,
                               SupplierId = 555,
                               ForeignCurrency = string.Empty,
                               Currency = "GBP",
                               CarrierId = 678,
                               TransportId = 1,
                               TransportBillNumber = string.Empty,
                               TransactionId = 44,
                               DeliveryTermCode = string.Empty,
                               ArrivalPort = "LAX",
                               ArrivalDate = null,
                               TotalImportValue = 123.4m,
                               Weight = null,
                               CustomsEntryCode = "code RED",
                               CustomsEntryCodeDate = null,
                               LinnDuty = null,
                               LinnVat = null,
                               DateCancelled = null,
                               CancelledBy = null,
                               CancelledReason = null,
                               NumCartons = null,
                               NumPallets = null,
                               Comments = string.Empty,
                               CreatedBy = null,
                               CustomsEntryCodePrefix = "AA",
                               Pva = "Y",
                               InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                               OrderDetails = new List<ImportBookOrderDetail>(),
                               PostEntries = new List<ImportBookPostEntry>()
                           };

            this.newImpBook = new ImportBook
                              {
                                  Id = this.impbookId,
                                  DateCreated = this.now.AddDays(2),
                                  ParcelNumber = 1,
                                  SupplierId = 556,
                                  ForeignCurrency = "Y",
                                  Currency = "YEN",
                                  CarrierId = 678,
                                  TransportId = 2,
                                  TransportBillNumber = "1212",
                                  TransactionId = 45,
                                  DeliveryTermCode = "dli",
                                  ArrivalPort = "LAZ",
                                  ArrivalDate = this.now.AddDays(3),
                                  TotalImportValue = 133.4m,
                                  Weight = 11.1m,
                                  CustomsEntryCode = "code green",
                                  CustomsEntryCodeDate = this.now.AddDays(2),
                                  LinnDuty = 12,
                                  LinnVat = 11.1m,
                                  DateCancelled = this.now.AddDays(5),
                                  CancelledBy = 33105,
                                  CancelledReason = "cancel",
                                  NumCartons = 1,
                                  NumPallets = 1,
                                  Comments = "now closed",
                                  CreatedBy = 33105,
                                  CustomsEntryCodePrefix = "AA",
                                  Pva = "N",
                                  ExchangeCurrency = "YEN",
                                  ExchangeRate = 15.929m,
                                  InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                                  OrderDetails = new List<ImportBookOrderDetail>(),
                                  PostEntries = new List<ImportBookPostEntry>()
                              };

            this.LedgerPeriodRepository.FindBy(Arg.Any<Expression<Func<LedgerPeriod, bool>>>())
                .Returns(new LedgerPeriod { PeriodNumber = 1234 });

            this.Sut.Update(this.impbook, this.newImpBook);
        }

        [Test]
        public void ShouldHaveUpdatedAllFieldsOnOriginalImportBook()
        {
            this.impbook.Id.Equals(this.impbookId).Should().BeTrue();
            this.impbook.ParcelNumber.Equals(this.newImpBook.ParcelNumber).Should().BeTrue();
            this.impbook.DateCreated.Equals(this.newImpBook.DateCreated).Should().BeFalse();
            this.impbook.DateCreated.Equals(this.impbook.DateCreated).Should().BeTrue();
            this.impbook.SupplierId.Equals(this.newImpBook.SupplierId).Should().BeTrue();
            this.impbook.ForeignCurrency.Equals(this.newImpBook.ForeignCurrency).Should().BeTrue();
            this.impbook.Currency.Equals(this.newImpBook.Currency).Should().BeTrue();
            this.impbook.CarrierId.Equals(this.newImpBook.CarrierId).Should().BeTrue();
            this.impbook.TransportId.Equals(this.newImpBook.TransportId).Should().BeTrue();
            this.impbook.TransportBillNumber.Equals(this.newImpBook.TransportBillNumber).Should().BeTrue();
            this.impbook.TransactionId.Equals(this.newImpBook.TransactionId).Should().BeTrue();
            this.impbook.DeliveryTermCode.Equals(this.newImpBook.DeliveryTermCode).Should().BeTrue();
            this.impbook.ArrivalPort.Equals(this.newImpBook.ArrivalPort).Should().BeTrue();
            this.impbook.ArrivalDate.Equals(this.newImpBook.ArrivalDate).Should().BeTrue();
            this.impbook.TotalImportValue.Equals(this.newImpBook.TotalImportValue).Should().BeTrue();
            this.impbook.Weight.Equals(this.newImpBook.Weight).Should().BeTrue();
            this.impbook.CustomsEntryCode.Equals(this.newImpBook.CustomsEntryCode).Should().BeTrue();
            this.impbook.CustomsEntryCodeDate.Equals(this.newImpBook.CustomsEntryCodeDate).Should().BeTrue();
            this.impbook.LinnDuty.Equals(this.newImpBook.LinnDuty).Should().BeTrue();
            this.impbook.LinnVat.Equals(this.newImpBook.LinnVat).Should().BeTrue();
            this.impbook.DateCancelled.Equals(this.newImpBook.DateCancelled).Should().BeTrue();
            this.impbook.CancelledBy.Equals(this.newImpBook.CancelledBy).Should().BeTrue();
            this.impbook.CancelledReason.Equals(this.newImpBook.CancelledReason).Should().BeTrue();
            this.impbook.NumCartons.Equals(this.newImpBook.NumCartons).Should().BeTrue();
            this.impbook.NumPallets.Equals(this.newImpBook.NumPallets).Should().BeTrue();
            this.impbook.Comments.Equals(this.newImpBook.Comments).Should().BeTrue();
            this.impbook.CreatedBy.Equals(this.newImpBook.CreatedBy).Should().BeTrue();
            this.impbook.CustomsEntryCodePrefix.Equals(this.newImpBook.CustomsEntryCodePrefix).Should().BeTrue();
            this.impbook.Pva.Equals("N").Should().BeTrue();
            this.impbook.PeriodNumber.Value.Should().Be(1234);
            this.impbook.Currency.Should().Be("YEN");
            this.impbook.ExchangeCurrency.Should().Be("YEN");
            this.impbook.ExchangeRate.Should().Be(15.929m);
        }
    }
}
