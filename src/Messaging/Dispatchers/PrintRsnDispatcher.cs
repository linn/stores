namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.MessageDispatch;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class PrintRsnDispatcher : IPrintRsnService
    {
        private readonly string contentType = "application/json";

        private readonly string routingKey = "orawin.rsn.print";

        private readonly IMessageDispatcher messageDispatcher;

        public PrintRsnDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void PrintRsn(int rsnNumber, string copy)
        {
            var resource = new PrintRsnMessageResource
                               {
                                   RsnNumber = rsnNumber,
                                   Copy = copy,
                                   Printer = "Invoice" // todo - find correct printer
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
