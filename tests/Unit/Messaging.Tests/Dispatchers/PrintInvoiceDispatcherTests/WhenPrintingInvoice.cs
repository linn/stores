namespace Linn.Stores.Messaging.Tests.Dispatchers.PrintInvoiceDispatcherTests
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingInvoice : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PrintInvoice(
                "invoice",
                "I",
                4321,
                false, 
                true);
        }

        [Test]
        public void ShouldSendMessage()
        {
            this.MessageDispatcher.Received().Dispatch(
                "print.invoice.document",
                Arg.Any<byte[]>(),
                "application/json");
        }
    }
}
