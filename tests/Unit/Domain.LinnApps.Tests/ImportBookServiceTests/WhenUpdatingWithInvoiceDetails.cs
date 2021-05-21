

namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.Parts;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class WhenUpdatingWithInvoiceDetails : ContextBase
    {
        private static readonly int impbookId = 12007;
        private ImportBook impbook;
        private IEnumerable<ImportBookInvoiceDetail> invoiceDetails;

        private ImportBookInvoiceDetail firstInvoiceDetail = new ImportBookInvoiceDetail() { ImportBookId = impbookId, InvoiceNumber = "123", LineNumber = 1 };
        private ImportBookInvoiceDetail secondInvoiceDetail = new ImportBookInvoiceDetail() { ImportBookId = impbookId, InvoiceNumber = "1234", LineNumber = 2};


        [SetUp]
        public void SetUp()
        {
            this.impbook = new ImportBook()
                               {
                                   Id = impbookId
            };
            this.invoiceDetails = new List<ImportBookInvoiceDetail> { this.firstInvoiceDetail, this.secondInvoiceDetail };

            var orderDetails = new List<ImportBookOrderDetail>();
            var postEntries = new List<ImportBookPostEntry>();


            //below doesn't actually work
            this.InvoiceDetailRepository.FindById(new ImportBookInvoiceDetailKey(impbookId, 1)).Returns(new ImportBookInvoiceDetail{ImportBookId = 12007});


            this.InvoiceDetailRepository.FindById(new ImportBookInvoiceDetailKey(impbookId, 2)).Returns(new ImportBookInvoiceDetail());

            this.Sut.Update(this.impbook, invoiceDetails, orderDetails, postEntries);
        }

        [Test]
        public void ShouldHaveAddedInvoiceDetail()
        {
            this.InvoiceDetailRepository.Received().Add(Arg.Any<ImportBookInvoiceDetail>());
        }

        [Test]
        public void ShouldHaveUpdatedInvoiceDetail()
        {
            //check updated somehow
        }
    }
}
