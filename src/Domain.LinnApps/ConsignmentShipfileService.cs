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
            var withDetails = this.GetEmailDetails(toSend);

            var consignmentShipfiles = withDetails as ConsignmentShipfile[] ?? withDetails.ToArray();
            foreach (var shipfile in consignmentShipfiles)
            {
                shipfile.Consignment =
                    this.consignmentRepository.FindBy(c => c.ConsignmentId == shipfile.ConsignmentId);
                if (shipfile.Message == null)
                {
                    var pdf = this.pdfBuilder.BuildPdf(shipfile); // todo - implement pdf builder properly
                    this.emailService.SendEmail( // todo - get proper email data from shipfile
                        "lewis.renfrew@linn.co.uk",
                        "Me",
                        "lewis.renfrew@linn.co.uk",
                        "Me",
                        "hello",
                        "hello",
                        pdf.Result);
                    shipfile.Message = ShipfileStatusMessages.EmailSent;
                }
            }

            return consignmentShipfiles;
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
                    // not an org
                    if (salesOrder.Account.OrgId == null)
                    {
                        shipfile.Message = salesOrder.Account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                    }
                    else // an org
                    {
                        shipfile.Message = null;
                    }
                }
            }

            return consignmentShipfiles;
        }
    }
}
