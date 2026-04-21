namespace Linn.Stores.Messaging.Tests.Dispatchers.PrintConsignmentNoteDispatcherTests
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingConsignmentNote : ContextBase
    {
        private int consignmentId;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 123;
            this.Sut.PrintConsignmentNote(this.consignmentId, "invoice");
        }

        [Test]
        public void ShouldSendMessage()
        {
            this.MessageDispatcher.Received().Dispatch(
                "print.packing-list.document",
                Arg.Any<byte[]>(),
                "application/json",
                Arg.Any<System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object, object>>>());
        }
    }
}
