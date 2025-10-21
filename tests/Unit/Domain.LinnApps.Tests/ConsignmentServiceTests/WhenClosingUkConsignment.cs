namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingUkConsignment : ContextBase
    {
        private int closedById = 123;

        [SetUp]
        public void SetUp()
        {
            this.Consignment.HubId = 1;
            this.ConsignmentProxyService.CanCloseAllocation(this.ConsignmentId)
                .Returns(new ProcessResult(true, "ok"));
            this.InvoicingPack.InvoiceConsignment(this.ConsignmentId, this.closedById)
                .Returns(new ProcessResult(true, "invoiced ok"));
            this.ExportBookPack.MakeExportBookFromConsignment(this.ConsignmentId)
                .Returns(new ProcessResult(true, "export book ok"));

            this.Sut.CloseConsignment(this.Consignment, this.closedById);
        }

        [Test]
        public void ShouldCallCanCloseConsignmentProxy()
        {
            this.ConsignmentProxyService.Received().CanCloseAllocation(this.Consignment.ConsignmentId);
        }

        [Test]
        public void ShouldInvoiceConsignment()
        {
            this.InvoicingPack.Received().InvoiceConsignment(this.Consignment.ConsignmentId, this.closedById);
        }

        [Test]
        public void ShouldMakeExportBook()
        {
            this.ExportBookPack.Received().MakeExportBookFromConsignment(this.Consignment.ConsignmentId);
        }

        [Test]
        public void ShouldNotPrintConsignmentNote()
        {
            this.PrintConsignmentNoteDispatcher.DidNotReceive().PrintConsignmentNote(Arg.Any<int>(), Arg.Any<string>());
        }

        [Test]
        public void ShouldPrintOneInvoiceViaPrintProxy()
        {
            // some temporary proxy tests before we move to a message solution
            this.PrintService.DidNotReceive().PrintDocument("http://test:printer", "I", 123, true, true);
            this.PrintService.Received().PrintDocument("http://test:printer", "I", 123, false, false);
        }

        [Test]
        public void ShouldNotPrintExportBook()
        {
            this.PrintInvoiceDispatcher.DidNotReceive().PrintInvoice(1, "E", "CUSTOMER MASTER", "Y", Arg.Any<string>());
        }
    }
}
