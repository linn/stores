namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingConsignment : ContextBase
    {
        private int closedById = 123;

        [SetUp]
        public void SetUp()
        {
            this.ConsignmentProxyService.CanCloseAllocation(this.ConsignmentId)
                .Returns(new ProcessResult(true, "ok"));
            this.InvoicingPack.InvoiceConsignment(this.ConsignmentId, this.closedById)
                .Returns(new ProcessResult(true, "invoiced ok"));

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
    }
}
