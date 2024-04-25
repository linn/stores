// namespace Linn.Stores.Messaging.Tests
// {
//     using Linn.Common.Messaging.RabbitMQ;
//     using Linn.Common.Messaging.RabbitMQ.Dispatchers;
//
//     using NSubstitute;
//
//     using NUnit.Framework;
//
//     public abstract class MessagingContextBase
//     {
//         protected IMessageDispatcher<> MessageDispatcher { get; private set; }
//
//         [SetUp]
//         public void EstablishBaseContext()
//         {
//             this.MessageDispatcher = Substitute.For<IMessageDispatcher>();
//         }
//     }
// }