namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using NUnit.Framework;

    public class WhenUpdatingWithInvoiceDetails : ContextBase
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

            var secondInvoiceDetail = new ImportBookInvoiceDetail
                                      {
                                          ImportBookId = this.impbookId,
                                          InvoiceNumber = "1234",
                                          LineNumber = 2,
                                          InvoiceValue = 155.2m
                                      };

            var updatedFirstInvoiceDetail = new ImportBookInvoiceDetail
                                            {
                                                ImportBookId = this.impbookId,
                                                InvoiceNumber = "133",
                                                LineNumber = 1,
                                                InvoiceValue = 125.5m
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
                               InvoiceDetails = new List<ImportBookInvoiceDetail> { firstInvoiceDetail },
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
                                    InvoiceDetails =
                                        new List<ImportBookInvoiceDetail>
                                        {
                                            updatedFirstInvoiceDetail, secondInvoiceDetail
                                        },
                                    OrderDetails = new List<ImportBookOrderDetail>(),
                                    PostEntries = new List<ImportBookPostEntry>()
                                };

            this.Sut.Update(this.impbook, newImportBook);
        }

        [Test]
        public void ShouldHaveUpdatedInvoiceDetail()
        {
            this.impbook.InvoiceDetails.FirstOrDefault(
                    x => x.LineNumber == 1 && x.InvoiceNumber == "133" && x.InvoiceValue == 125.5m).Should()
                .NotBeNull();
        }

        [Test]
        public void ShouldHaveAddedInvoiceDetail()
        {
            this.impbook.InvoiceDetails.Count().Should().Be(2);
            this.impbook.InvoiceDetails.FirstOrDefault(
                    x => x.LineNumber == 2 && x.InvoiceNumber == "1234" && x.InvoiceValue == 155.2m).Should()
                .NotBeNull();
        }
    }
}
