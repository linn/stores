namespace Linn.Stores.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.MessageDispatch;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class PrintRsnDispatcher : IPrintRsnService
    {
        private readonly string contentType = "application/json";

        private readonly string routingKey = "orawin.rsn.print";

        private readonly IMessageDispatcher messageDispatcher;

        private IRepository<PrinterMapping, int> printerRepository;

        public PrintRsnDispatcher(
            IMessageDispatcher messageDispatcher, 
            IRepository<PrinterMapping, int> printerRepository)
        {
            this.messageDispatcher = messageDispatcher;
            this.printerRepository = printerRepository;
        }

        public void PrintRsn(int rsnNumber, int userNumber, string copy)
        {
            var printer = this.printerRepository
                .FindBy(a => a.UserNumber == userNumber && a.PrinterGroup == "GOODS-IN")?.PrinterName;

            if (string.IsNullOrEmpty(printer))
            {
                 printer = this.printerRepository.FindBy(
                    a => a.DefaultForGroup == "Y" && a.PrinterGroup == "GOODS-IN").PrinterName;
            }

            var resource = new PrintRsnMessageResource
                               {
                                   RsnNumber = rsnNumber,
                                   Copy = copy,
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
