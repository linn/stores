namespace Linn.Stores.Messaging.Tests.Dispatchers.PrintInvoiceDispatcherTests
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingInvoice : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PrintInvoice(4321, "I", "MASTER", "Y", "invoice");
        }

        [Test]
        public void ShouldSendMessage()
        {
            this.MessageDispatcher.Received().Dispatch(
                "orawin.invoice.print",
                Arg.Any<byte[]>(),
                "application/json");
        }
    }
}
