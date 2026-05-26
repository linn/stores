namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Resources.MessageDispatch;

    using Newtonsoft.Json;

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
            string printerGroup,
            string documentType,
            int documentNumber,
            bool showTermsAndConditions,
            bool showPrices)
        {
            var messageBody = new PrintInvoiceDocumentMessageBody
                                  {
                                      DocumentNumber = documentNumber,
                                      DocumentType = documentType ?? string.Empty,
                                      ShowTermsAndConditions = showTermsAndConditions,
                                      ShowPrices = showPrices,
                                      PrinterGroup = printerGroup ?? string.Empty
                                  };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));

            this.messageDispatcher.Dispatch(this.routingKey, body, this.contentType);
        }
    }
}
