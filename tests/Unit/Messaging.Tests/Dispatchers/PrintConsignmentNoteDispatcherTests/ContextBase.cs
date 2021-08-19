namespace Linn.Stores.Messaging.Tests.Dispatchers.PrintConsignmentNoteDispatcherTests
{
    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Messaging.Dispatchers;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IPrintConsignmentNoteDispatcher Sut { get; private set; }

        protected IMessageDispatcher MessageDispatcher { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.MessageDispatcher = Substitute.For<IMessageDispatcher>();
            this.Sut = new PrintConsignmentNoteDispatcher(this.MessageDispatcher);
        }
    }
}
