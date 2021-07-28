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

        private readonly string routingKey = "orawin.invoice.print";

        private readonly IMessageDispatcher messageDispatcher;

        public PrintInvoiceDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void PrintInvoice(
            int documentNumber,
            string documentType,
            string copyType,
            string showPrices,
            string printer)
        {
            var resource = new PrintInvoiceMessageResource
                               {
                                   DocumentNumber = documentNumber,
                                   DocumentType = documentType,
                                   CopyType = copyType,
                                   ShowPrices = showPrices,
                                   Printer = printer
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
