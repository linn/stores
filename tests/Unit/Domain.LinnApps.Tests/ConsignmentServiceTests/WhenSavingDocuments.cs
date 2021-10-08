namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSavingDocuments : ContextBase
    {
        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.SaveConsignmentDocuments(this.ConsignmentId);
        }

        [Test]
        public void ShouldSaveInvoices()
        {
            this.PrintInvoiceDispatcher.Received().SaveInvoice(123, "I", "CUSTOMER MASTER", "Y", "Invoice 123.pdf");
            this.PrintInvoiceDispatcher.Received().SaveInvoice(456, "I", "CUSTOMER MASTER", "Y", "Invoice 456.pdf");
        }

        [Test]
        public void ShouldSavePackingList()
        {
            this.PrintConsignmentNoteDispatcher.Received().SaveConsignmentNote(808, "Packing List 808.pdf");
        }
    }
}
