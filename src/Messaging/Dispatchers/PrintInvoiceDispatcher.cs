namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Resources.MessageDispatch;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

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
            var resource = new PrintInvoiceMessageResource
                               {
                                   DocumentNumber = documentNumber,
                                   DocumentType = documentType,
                                   ShowTermsAndConditions = showTermsAndConditions, 
                                   ShowPrices = showPrices,
                                   Printer = printerUri
                               };

            var json = JsonConvert.SerializeObject(
                resource,
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            var body = Encoding.UTF8.GetBytes(json);

            this.messageDispatcher.Dispatch(this.routingKey, body, this.contentType);
        }
    }
}
