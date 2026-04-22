namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Collections.Generic;
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.Dispatchers;

    public class PrintInvoiceDispatcher : IPrintInvoiceDispatcher
    {
        private readonly string contentType = "application/json";

        private readonly string routingKey = "print.invoice.document";

        private readonly IMessageDispatcher messageDispatcher;

        public PrintInvoiceDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void PrintInvoice(
            string printerUri,
            string documentType,
            int documentNumber,
            bool showTermsAndConditions,
            bool showPrices)
        {
            var headers = new List<KeyValuePair<object, object>>
                              {
                                  new KeyValuePair<object, object>("documentNumber", documentNumber.ToString()),
                                  new KeyValuePair<object, object>("documentType", documentType ?? string.Empty),
                                  new KeyValuePair<object, object>("showTermsAndConditions", showTermsAndConditions.ToString()),
                                  new KeyValuePair<object, object>("showPrices", showPrices.ToString()),
                                  new KeyValuePair<object, object>("printerUri", printerUri ?? string.Empty)
                              };

            var body = Encoding.UTF8.GetBytes(string.Empty);

            this.messageDispatcher.Dispatch(this.routingKey, body, this.contentType, headers);
        }
    }
}
