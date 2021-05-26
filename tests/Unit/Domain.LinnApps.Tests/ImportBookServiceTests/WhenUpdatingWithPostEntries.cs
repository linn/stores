namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using NUnit.Framework;

    public class WhenUpdatingWithPostEntries : ContextBase
    {
        private readonly int impbookId = 12007;
        private readonly DateTime now = DateTime.Now;
        private ImportBook impbook;

        [SetUp]
        public void SetUp()
        {
            var firstPostEntry = new ImportBookPostEntry()
                                 {
                                     ImportBookId = this.impbookId,
                                     LineNumber = 1,
                                     EntryCodePrefix = "PR",
                                     EntryCode = "code blu",
                                     EntryDate = null,
                                     Reference = "refer fence",
                                     Duty = null,
                                     Vat = null
                                 };

            var secondPostEntry = new ImportBookPostEntry()
                                  {
                                      ImportBookId = this.impbookId,
                                      LineNumber = 2,
                                      EntryCodePrefix = "DL",
                                      EntryCode = "code blanc",
                                      EntryDate = this.now.AddDays(-6),
                                      Reference = "hocus pocus",
                                      Duty = 33,
                                      Vat = 44
                                  };

            var updatedFirstPostEntry = new ImportBookPostEntry()
                                        {
                                            ImportBookId = this.impbookId,
                                            LineNumber = 1,
                                            EntryCodePrefix = "PRE",
                                            EntryCode = "code vert",
                                            EntryDate = this.now.AddDays(-5),
                                            Reference = "refer fence",
                                            Duty = 111,
                                            Vat = 222
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
                               PostEntries = new List<ImportBookPostEntry> { firstPostEntry }
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
                                    OrderDetails = new List<ImportBookOrderDetail>(),
                                    PostEntries =
                                        new List<ImportBookPostEntry> { updatedFirstPostEntry, secondPostEntry }
                                };

            this.Sut.Update(this.impbook, newImportBook);
        }

        [Test]
        public void ShouldHaveUpdatedInvoiceDetail()
        {
            this.impbook.PostEntries.FirstOrDefault(
                x => x.ImportBookId == this.impbookId && x.LineNumber == 1 && x.EntryCodePrefix == "PRE"
                     && x.EntryCode == "code vert" && x.EntryDate == this.now.AddDays(-5)
                     && x.Reference == "refer fence" && x.Duty == 111 && x.Vat == 222).Should().NotBeNull();
        }

        [Test]
        public void ShouldHaveAddedInvoiceDetail()
        {
            this.impbook.PostEntries.Count().Should().Be(2);
            this.impbook.PostEntries.FirstOrDefault(
                x => x.ImportBookId == this.impbookId && x.LineNumber == 2 && x.EntryCodePrefix == "DL"
                     && x.EntryCode == "code blanc" && x.EntryDate == this.now.AddDays(-6)
                     && x.Reference == "hocus pocus" && x.Duty == 33 && x.Vat == 44).Should().NotBeNull();
        }
    }
}
