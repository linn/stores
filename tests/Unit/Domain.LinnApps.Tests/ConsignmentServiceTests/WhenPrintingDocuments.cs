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
            this.result = this.Sut.PrintConsignmentDocuments(this.ConsignmentId, this.userNumber);
        }

        [Test]
        public void ShouldPrintInvoices()
        {
            this.PrintService.Received().PrintDocument("http://test:printer", "I", 123, false, false);
            this.PrintService.Received().PrintDocument("http://test:printer", "I", 456, false, false);
        }
    }
}
