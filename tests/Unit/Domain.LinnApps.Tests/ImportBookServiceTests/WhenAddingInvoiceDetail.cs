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

    public class WhenAddingInvoiceDetail : ContextBase
    {
        private readonly int impbookId = 12007;

        private ImportBook impbook;

        [SetUp]
        public void SetUp()
        {
            var firstInvoiceDetail = new ImportBookInvoiceDetail
                                         {
                                             ImportBookId = this.impbookId,
                                             InvoiceNumber = "123",
                                             LineNumber = 1,
                                             InvoiceValue = 12.5m
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
                                   OrderDetails = new List<ImportBookOrderDetail>(),
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
                                        InvoiceDetails = new List<ImportBookInvoiceDetail> { firstInvoiceDetail },
                                        OrderDetails = new List<ImportBookOrderDetail>(),
                                        PostEntries = new List<ImportBookPostEntry>()
                                    };

            this.LedgerPeriodRepository.FindBy(Arg.Any<Expression<Func<LedgerPeriod, bool>>>())
                .Returns(new LedgerPeriod { PeriodNumber = 1234 });

            this.Sut.Update(this.impbook, newImportBook);
        }

        [Test]
        public void ShouldHaveAddedInvoiceDetail()
        {
            this.impbook.InvoiceDetails.Count().Should().Be(1);
            this.impbook.InvoiceDetails.FirstOrDefault(
                x => x.LineNumber == 1 && x.InvoiceNumber == "123" && x.InvoiceValue == 12.5m).Should().NotBeNull();
        }
    }
}
