namespace Linn.Stores.Messaging.Messages
{
    using Linn.Common.Messaging.RabbitMQ.Messages;

    using RabbitMQ.Client.Events;

    public class PrintInvoiceMessage : RabbitMessage
    {
        public const string RoutingKey = "orawin.invoice.print";

        public PrintInvoiceMessage(BasicDeliverEventArgs e) : base(e)
        {
        }
    }
}
