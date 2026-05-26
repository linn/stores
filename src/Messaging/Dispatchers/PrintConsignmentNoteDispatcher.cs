namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Resources.MessageDispatch;

    using Newtonsoft.Json;

    public class PrintConsignmentNoteDispatcher : IPrintConsignmentNoteDispatcher
    {
        private readonly string contentType = "application/json";

        private readonly string routingKey = "print.packing-list.document";

        private readonly IMessageDispatcher messageDispatcher;

        public PrintConsignmentNoteDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void PrintConsignmentNote(int consignmentId, string printerGroup)
        {
            var messageBody = new PrintPackingListMessageBody
                                  {
                                      ConsignmentId = consignmentId,
                                      PrinterGroup = printerGroup ?? string.Empty
                                  };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));

            this.messageDispatcher.Dispatch(this.routingKey, body, this.contentType);
        }
    }
}
