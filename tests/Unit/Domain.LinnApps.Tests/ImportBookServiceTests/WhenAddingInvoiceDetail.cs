namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using NUnit.Framework;


    public class WhenAddingInvoiceDetail : ContextBase
    {
        private readonly int impbookId = 12007;
        private ImportBook impbook;


        [SetUp]
        public void SetUp()
        {
            var firstInvoiceDetail = new ImportBookInvoiceDetail() { ImportBookId = impbookId, InvoiceNumber = "123", LineNumber = 1, InvoiceValue = (decimal)12.5 };

            this.impbook = new ImportBook
                               {
                                   Id = this.impbookId,
                                   DateCreated = DateTime.Now.AddDays(-5),
                                   SupplierId = 555,
                                   CarrierId = 678,
                                   TransportId = 1,
                                   TransactionId = 44,
                                   TotalImportValue = (decimal)123.4,
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
                TotalImportValue = (decimal)123.4,
                InvoiceDetails = new List<ImportBookInvoiceDetail> { firstInvoiceDetail },
                OrderDetails = new List<ImportBookOrderDetail>(),
                PostEntries = new List<ImportBookPostEntry>()
            };


            this.Sut.Update(this.impbook, newImportBook);
        }

        [Test]
        public void ShouldHaveAddedInvoiceDetail()
        {
            this.impbook.InvoiceDetails.Count().Should().Be(1);
            this.impbook.InvoiceDetails.FirstOrDefault(
                    x => x.LineNumber == 1 && x.InvoiceNumber == "123" && x.InvoiceValue == (decimal)12.5)
                .Should().NotBeNull();
        }
    }
}
