namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndLineHasOrderNumberAndRsnNumber : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            var candidate = new ImportBook
            {
                Id = 12007,
                DateCreated = DateTime.Today,
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
                ExchangeCurrency = "GBP",
                ExchangeRate = 1m,
                InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                OrderDetails = new List<ImportBookOrderDetail>
                                   {
                                       new ImportBookOrderDetail
                                           {
                                               RsnNumber = 1,
                                               OrderNumber = 2
                                           }
                                   },
                PostEntries = new List<ImportBookPostEntry>()
            };



            this.LedgerPeriodRepository.FindBy(Arg.Any<Expression<Func<LedgerPeriod, bool>>>())
                .Returns(new LedgerPeriod { PeriodNumber = 1234 });

            this.action = () => this.Sut.Create(candidate);
        }

        [Test]
        public void ShouldThrow()
        {
            this.action.Should().Throw<ImportBookException>()
                .WithMessage("Detail lines cannot specify both an order and an rsn.");
        }
    }
}
