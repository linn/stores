namespace Linn.Stores.Messaging.Messages
{
    using Linn.Common.Messaging.RabbitMQ.Messages;

    using RabbitMQ.Client.Events;

    public class PrintConsignmentNoteMessage : RabbitMessage
    {
        public const string RoutingKey = "orawin.consignment-note.print";

        public PrintConsignmentNoteMessage(BasicDeliverEventArgs e) : base(e)
        {
        }
    }
}
