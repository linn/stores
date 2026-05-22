namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.MessageDispatch;

    using Newtonsoft.Json;

    public class PrintRsnDispatcher : IPrintRsnService
    {
        private readonly string contentType = "application/json";

        private readonly string routingKey = "print.rsn.document";

        private readonly IMessageDispatcher messageDispatcher;

        public PrintRsnDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void PrintRsn(int rsnNumber, int userNumber, string copy, string facilityCode = null)
        {
            var messageBody = new PrintRsnDocumentMessageBody
                                  {
                                      RsnNumber = rsnNumber,
                                      CopyType = copy,
                                      FacilityCode = facilityCode ?? string.Empty,
                                      PrinterGroup = "GOODS-IN"
                                  };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));

            this.messageDispatcher.Dispatch(this.routingKey, body, this.contentType);
        }
    }
}
