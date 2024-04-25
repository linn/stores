namespace Linn.Stores.Messaging.Messages
{
    using Linn.Common.Messaging.RabbitMQ.Messages;

    using RabbitMQ.Client.Events;

    public class PrintRsnMessage : RabbitMessage
    {
        public const string RoutingKey = "orawin.rsn.print";

        public PrintRsnMessage(BasicDeliverEventArgs e) : base(e)
        {
        }
    }
}
