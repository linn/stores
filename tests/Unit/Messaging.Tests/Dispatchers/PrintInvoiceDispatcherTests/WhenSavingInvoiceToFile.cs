namespace Linn.Stores.Messaging.Tests.Dispatchers.PrintInvoiceDispatcherTests
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenSavingInvoiceToFile : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PrintInvoice(4321, "I", "MASTER", "Y", "file1");
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
