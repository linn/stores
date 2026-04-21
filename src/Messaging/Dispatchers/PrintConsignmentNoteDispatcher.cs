namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Collections.Generic;
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.Dispatchers;

    public class PrintConsignmentNoteDispatcher : IPrintConsignmentNoteDispatcher
    {
        private readonly string contentType = "application/json";

        private readonly string routingKey = "print.packing-list.document";

        private readonly IMessageDispatcher messageDispatcher;

        public PrintConsignmentNoteDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void PrintConsignmentNote(int consignmentId, string printerUri)
        {
            var headers = new List<KeyValuePair<object, object>>
                              {
                                  new KeyValuePair<object, object>("consignmentId", consignmentId.ToString()),
                                  new KeyValuePair<object, object>("printerUri", printerUri ?? string.Empty)
                              };

            var body = Encoding.UTF8.GetBytes(string.Empty);

            this.messageDispatcher.Dispatch(this.routingKey, body, this.contentType, headers);
        }
    }
}
