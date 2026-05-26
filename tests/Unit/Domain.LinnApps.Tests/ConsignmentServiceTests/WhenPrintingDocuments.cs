namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingDocuments : ContextBase
    {
        private int userNumber;

        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.userNumber = 12345;
            this.result = this.Sut.PrintConsignmentDocuments(this.ConsignmentId);
        }

        [Test]
        public void ShouldPrintInvoices()
        {
            this.PrintInvoiceDispatcher.Received().PrintInvoice("DISPATCH-INVOICE", "I", 123,  false, false);
            this.PrintInvoiceDispatcher.Received().PrintInvoice("DISPATCH-INVOICE", "I", 456, false, false);
        }
    }
}
