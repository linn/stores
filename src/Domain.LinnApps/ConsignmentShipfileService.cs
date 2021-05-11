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

        public ConsignmentShipfileService(
            IQueryRepository<SalesOrder> salesOrderRepository,
            IEmailService emailService,
            IShipfilePdfBuilder pdfBuilder)
        {
            this.salesOrderRepository = salesOrderRepository;
            this.emailService = emailService;
            this.pdfBuilder = pdfBuilder;
        }

        public IEnumerable<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend)
        {
            var withDetails = this.GetEmailDetails(toSend);

            var consignmentShipfiles = withDetails as ConsignmentShipfile[] ?? withDetails.ToArray();
            foreach (var shipfile in consignmentShipfiles)
            {
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

                    }
                }
            }

            return consignmentShipfiles;
        }
    }
}
