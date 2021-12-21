namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreating : ContextBase
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



            this.LedgerPeriodRepository.FindBy(Arg.Any<Expression<Func<LedgerPeriod, bool>>>())
                .Returns(new LedgerPeriod { PeriodNumber = 1234 });

            this.newImpBook = this.Sut.Create(this.impbook);
        }

        [Test]
        public void ShouldHaveAddedPeriodNumber()
        {
            this.newImpBook.PeriodNumber.Value.Should().Be(1234);
        }
    }
}
