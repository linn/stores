namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Resources.MessageDispatch;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class PrintConsignmentNoteDispatcher : IPrintConsignmentNoteDispatcher
    {
        private const string ContentType = "application/json";

        private readonly string routingKey = "orawin.consignment-note.print";

        private readonly IMessageDispatcher messageDispatcher;

        public PrintConsignmentNoteDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void PrintConsignmentNote(int consignmentId)
        {
            var resource = new PrintConsignmentNoteMessageResource
                               {
                                   ConsignmentId = consignmentId
                               };

            var json = JsonConvert.SerializeObject(
                resource,
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            var body = Encoding.UTF8.GetBytes(json);

            this.messageDispatcher.Dispatch(this.routingKey, body, ContentType);
        }
    }
}
