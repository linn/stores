namespace Linn.Stores.Messaging.Tests.Dispatchers.PrintConsignmentNoteDispatcherTests
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenSavingConsignmentNote : ContextBase
    {
        private int consignmentId;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 123;
            this.Sut.SaveConsignmentNote(this.consignmentId, "file1");
        }

        [Test]
        public void ShouldSendMessage()
        {
            this.MessageDispatcher.Received().Dispatch(
                "orawin.consignment-note.print",
                Arg.Any<byte[]>(),
                "application/json");
        }
    }
}
