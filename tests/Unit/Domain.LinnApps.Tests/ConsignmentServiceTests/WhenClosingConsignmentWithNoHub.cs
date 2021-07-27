namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingConsignmentWithNoHub : ContextBase
    {
        private int closedById = 123;

        [SetUp]
        public void SetUp()
        {
            this.Consignment.HubId = null;
            this.ConsignmentProxyService.CanCloseAllocation(this.ConsignmentId)
                .Returns(new ProcessResult(true, "ok"));
            this.InvoicingPack.InvoiceConsignment(this.ConsignmentId, this.closedById)
                .Returns(new ProcessResult(true, "invoiced ok"));

            this.Sut.CloseConsignment(this.Consignment, this.closedById);
        }

        [Test]
        public void ShouldNotMakeExportBook()
        {
            this.ExportBookPack.DidNotReceive().MakeExportBookFromConsignment(Arg.Any<int>());
        }
    }
}
