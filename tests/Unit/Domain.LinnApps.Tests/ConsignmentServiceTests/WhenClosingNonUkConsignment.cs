namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.ExportBooks;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingNonUkConsignment : ContextBase
    {
        private int closedById = 123;

        [SetUp]
        public void SetUp()
        {
            this.Consignment.HubId = 1;
            this.ExportBookRepository.FilterBy(Arg.Any<Expression<Func<ExportBook, bool>>>())
                .Returns(new List<ExportBook>
                             {
                                 new ExportBook { ExportId = 1, ConsignmentId = this.ConsignmentId }
                             }.AsQueryable());

            this.Consignment.Address.Country.CountryCode = "US";
            this.ConsignmentProxyService.CanCloseAllocation(this.ConsignmentId)
                .Returns(new ProcessResult(true, "ok"));
            this.InvoicingPack.InvoiceConsignment(this.ConsignmentId, this.closedById)
                .Returns(new ProcessResult(true, "invoiced ok"));
            this.ExportBookPack.MakeExportBookFromConsignment(this.ConsignmentId)
                .Returns(new ProcessResult(true, "export book ok"));

            this.Sut.CloseConsignment(this.Consignment, this.closedById);
        }

        [Test]
        public void ShouldPrintConsignmentNote()
        {
            this.PrintConsignmentNoteDispatcher.Received().PrintConsignmentNote(this.ConsignmentId, Arg.Any<string>());
        }

        [Test]
        public void ShouldPrintExportBookViaPrintProxyService()
        {
            // some temporary proxy tests before we move to a message solution
            this.PrintService.Received().PrintDocument("http://test:printer", "E", 1, true, true);
        }

        [Test]
        public void ShouldPrintTwoInvoicesViaPrintProxyService()
        {
            // some temporary proxy tests before we move to a message solution
            this.PrintService.Received().PrintDocument("http://test:printer", "I", 123, false, false);
            this.PrintService.Received().PrintDocument("http://test:printer", "I", 123, false, true);
        }
    }
}
