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
        public async Task SetUp()
        {
            this.userNumber = 12345;
            this.result = await this.Sut.PrintConsignmentDocuments(this.ConsignmentId, this.userNumber);
        }

        [Test]
        public void ShouldPrintInvoices()
        {
            this.PrintInvoiceDispatcher.Received().PrintInvoice(123, "I", "CUSTOMER MASTER", "Y", "Invoice");
            this.PrintInvoiceDispatcher.Received().PrintInvoice(456, "I", "CUSTOMER MASTER", "Y", "Invoice");
        }
    }
}
