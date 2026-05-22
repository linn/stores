namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Collections.Generic;
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class PrintRsnDispatcher : IPrintRsnService
    {
        private readonly string contentType = "application/json";

        private readonly string routingKey = "print.rsn.document";

        private readonly IMessageDispatcher messageDispatcher;

        private IRepository<PrinterMapping, int> printerRepository;

        public PrintRsnDispatcher(
            IMessageDispatcher messageDispatcher, 
            IRepository<PrinterMapping, int> printerRepository)
        {
            this.messageDispatcher = messageDispatcher;
            this.printerRepository = printerRepository;
        }

        public void PrintRsn(int rsnNumber, int userNumber, string copy, string facilityCode = null)
        {
            var headers = new List<KeyValuePair<object, object>>
                              {
                                  new KeyValuePair<object, object>("rsnNumber", rsnNumber.ToString()),
                                  new KeyValuePair<object, object>("copyType", copy),
                                  new KeyValuePair<object, object>("facilityCode", facilityCode ?? string.Empty),
                                  new KeyValuePair<object, object>("printerGroup", "GOODS-IN")
                              };

            var body = Encoding.UTF8.GetBytes(string.Empty);

            this.messageDispatcher.Dispatch(this.routingKey, body, this.contentType, headers);
        }
    }
}
