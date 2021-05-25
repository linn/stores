namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using NUnit.Framework;

    public class WhenRemovingInvoiceDetail : ContextBase
    {
        private readonly int impbookId = 12007;
        private ImportBook impbook;


        [SetUp]
        public void SetUp()
        {
            var firstInvoiceDetail = new ImportBookInvoiceDetail() { ImportBookId = impbookId, InvoiceNumber = "123", LineNumber = 1, InvoiceValue = (decimal)12.5 };

            var secondInvoiceDetail = new ImportBookInvoiceDetail() { ImportBookId = impbookId, InvoiceNumber = "1234", LineNumber = 2, InvoiceValue = (decimal)155.2 };
            
            this.impbook = new ImportBook
            {
                Id = this.impbookId,
                DateCreated = DateTime.Now.AddDays(-5),
                SupplierId = 555,
                CarrierId = 678,
                TransportId = 1,
                TransactionId = 44,
                TotalImportValue = (decimal)123.4,
                InvoiceDetails = new List<ImportBookInvoiceDetail> { firstInvoiceDetail, secondInvoiceDetail },
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
                TotalImportValue = (decimal)123.4,
                InvoiceDetails = new List<ImportBookInvoiceDetail> { firstInvoiceDetail },
                OrderDetails = new List<ImportBookOrderDetail>(),
                PostEntries = new List<ImportBookPostEntry>()
            };


            this.Sut.Update(this.impbook, newImportBook);
        }

        [Test]
        public void ShouldNotHaveRemovedInvoiceDetailAsFinancialRecord()
        {
            this.impbook.InvoiceDetails.Count().Should().Be(2);
        }
    }
}
