namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ImportBooks;

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
                                  ForeignCurrency = "YN",
                                  Currency = "GBD",
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
                                  InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                                  OrderDetails = new List<ImportBookOrderDetail>(),
                                  PostEntries = new List<ImportBookPostEntry>()
                              };

            this.Sut.Update(this.impbook, this.newImpBook);
        }

        [Test]
        public void ShouldHaveUpdatedAllFieldsOnOriginalImportBook()
        {
            this.impbook.Id.Equals(this.impbookId).Should().Be(true);
            this.impbook.ParcelNumber.Equals(this.newImpBook.ParcelNumber).Should().Be(true);
            this.impbook.DateCreated.Equals(this.newImpBook.DateCreated).Should().Be(false);
            this.impbook.DateCreated.Equals(this.impbook.DateCreated).Should().Be(true);
            this.impbook.SupplierId.Equals(this.newImpBook.SupplierId).Should().Be(true);
            this.impbook.ForeignCurrency.Equals(this.newImpBook.ForeignCurrency).Should().Be(true);
            this.impbook.Currency.Equals(this.newImpBook.Currency).Should().Be(true);
            this.impbook.CarrierId.Equals(this.newImpBook.CarrierId).Should().Be(true);
            this.impbook.TransportId.Equals(this.newImpBook.TransportId).Should().Be(true);
            this.impbook.TransportBillNumber.Equals(this.newImpBook.TransportBillNumber).Should().Be(true);
            this.impbook.TransactionId.Equals(this.newImpBook.TransactionId).Should().Be(true);
            this.impbook.DeliveryTermCode.Equals(this.newImpBook.DeliveryTermCode).Should().Be(true);
            this.impbook.ArrivalPort.Equals(this.newImpBook.ArrivalPort).Should().Be(true);
            this.impbook.ArrivalDate.Equals(this.newImpBook.ArrivalDate).Should().Be(true);
            this.impbook.TotalImportValue.Equals(this.newImpBook.TotalImportValue).Should().Be(true);
            this.impbook.Weight.Equals(this.newImpBook.Weight).Should().Be(true);
            this.impbook.CustomsEntryCode.Equals(this.newImpBook.CustomsEntryCode).Should().Be(true);
            this.impbook.CustomsEntryCodeDate.Equals(this.newImpBook.CustomsEntryCodeDate).Should().Be(true);
            this.impbook.LinnDuty.Equals(this.newImpBook.LinnDuty).Should().Be(true);
            this.impbook.LinnVat.Equals(this.newImpBook.LinnVat).Should().Be(true);
            this.impbook.DateCancelled.Equals(this.newImpBook.DateCancelled).Should().Be(true);
            this.impbook.CancelledBy.Equals(this.newImpBook.CancelledBy).Should().Be(true);
            this.impbook.CancelledReason.Equals(this.newImpBook.CancelledReason).Should().Be(true);
            this.impbook.NumCartons.Equals(this.newImpBook.NumCartons).Should().Be(true);
            this.impbook.NumPallets.Equals(this.newImpBook.NumPallets).Should().Be(true);
            this.impbook.Comments.Equals(this.newImpBook.Comments).Should().Be(true);
            this.impbook.CreatedBy.Equals(this.newImpBook.CreatedBy).Should().Be(true);
            this.impbook.CustomsEntryCodePrefix.Equals(this.newImpBook.CustomsEntryCodePrefix).Should().Be(true);
        }
    }
}
