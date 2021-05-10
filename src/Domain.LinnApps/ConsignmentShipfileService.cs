namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Common.Persistence;
    using System.Collections.Generic;
    using System.Linq;

    public class ConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        private readonly IEmailService emailService;

        private readonly IShipfilePdfBuilder pdfBuilder;

        public ConsignmentShipfileService(
            IQueryRepository<SalesOrder> salesOrderRepository,
            IEmailService emailService,
            IShipfilePdfBuilder pdfBuilder)
        {
            this.salesOrderRepository = salesOrderRepository;
            this.emailService = emailService;
            this.pdfBuilder = pdfBuilder;
        }

        public IEnumerable<ConsignmentShipfile> GetEmailDetails(IEnumerable<ConsignmentShipfile> shipfiles)
        {
            var consignmentShipfiles = shipfiles as ConsignmentShipfile[] ?? shipfiles.ToArray();
            foreach (var shipfile in consignmentShipfiles)
            {
                var orders = this.salesOrderRepository.FilterBy(
                        o => o.ConsignmentItems.Any() 
                             && o.ConsignmentItems.All(i => i.ConsignmentId == shipfile.Id))
                    .ToList();

                foreach (var salesOrder in orders)
                {
                    // not an org
                    if (salesOrder.Account.OrgId == null)
                    {
                        shipfile.Message = salesOrder.Account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                    }
                    else // an org
                    {

                    }
                }
            }

            return consignmentShipfiles;
        }

        public async void SendEmails(IEnumerable<ConsignmentShipfile> toSend)
        {
            // should I parallelize this loop? Does it take long enough to hurt
            foreach (var shipfile in toSend)
            {
                var pdf = await this.pdfBuilder.BuildPdf(shipfile);
                this.emailService.SendEmail(
                    "lewis.renfrew@linn.co.uk",
                    "Me",
                    "lewis.renfrew@linn.co.uk",
                    "Me",
                    "hello",
                    "hello",
                    pdf);
            }
        }
    }
}
