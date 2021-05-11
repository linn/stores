namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;

    public class ConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        private readonly IEmailService emailService;

        private readonly IShipfilePdfBuilder pdfBuilder;

        private readonly IQueryRepository<Consignment> consignmentRepository;

        public ConsignmentShipfileService(
            IQueryRepository<SalesOrder> salesOrderRepository,
            IEmailService emailService,
            IShipfilePdfBuilder pdfBuilder,
            IQueryRepository<Consignment> consignmentRepository)
        {
            this.salesOrderRepository = salesOrderRepository;
            this.emailService = emailService;
            this.pdfBuilder = pdfBuilder;
            this.consignmentRepository = consignmentRepository;
        }

        public IEnumerable<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend)
        {
            var withDetails = this.GetEmailDetails(toSend).ToList();

            foreach (var shipfile in withDetails)
            {
                shipfile.Consignment =
                    this.consignmentRepository.FindBy(c => c.ConsignmentId == shipfile.ConsignmentId);

                // a message implies there was some issue with finding contact details etc.
                if (shipfile.Message != null) 
                {
                    continue;
                }

                var pdf = this.pdfBuilder.BuildPdf(shipfile); 
                this.emailService.SendEmail(
                    "lewis.renfrew@linn.co.uk",
                    "Me",
                    "lewis.renfrew@linn.co.uk",
                    "Me",
                    "Consignment Shipfile",
                    $"Here is your pdf shipfile {shipfile.Consignment.CustomerName}",
                    pdf.Result);
                
                shipfile.Message = ShipfileStatusMessages.EmailSent;
            }

            return withDetails;
        }

        private IEnumerable<ConsignmentShipfile> GetEmailDetails(IEnumerable<ConsignmentShipfile> shipfiles)
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
                    if (salesOrder.Account.OrgId == null)
                    {
                        shipfile.Message = salesOrder.Account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                    }
                    else
                    {
                        shipfile.Message = null;
                    }
                }
            }

            return consignmentShipfiles;
        }
    }
}
