namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;

    public class ConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IEmailService emailService;

        private readonly IShipfilePdfBuilder pdfBuilder;

        private readonly IRepository<ConsignmentShipfile, int> shipfileRepository;

        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        public ConsignmentShipfileService(
            IEmailService emailService,
            IShipfilePdfBuilder pdfBuilder,
            IRepository<ConsignmentShipfile, int> shipfileRepository,
            IQueryRepository<SalesOrder> salesOrderRepository)
        {
            this.emailService = emailService;
            this.pdfBuilder = pdfBuilder;
            this.shipfileRepository = shipfileRepository;
            this.salesOrderRepository = salesOrderRepository;
        }

        public IEnumerable<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend)
        {
            var withDetails = this.GetEmailDetails(toSend).ToList();

            foreach (var shipfile in withDetails)
            {
                // a message implies there was some issue with finding contact details etc.
                if (shipfile.Message != null) 
                {
                    continue;
                }

                var emailAddress = shipfile.Consignment.SalesAccount.ContactDetails?.EmailAddress;
                var pdf = this.pdfBuilder.BuildPdf(shipfile); 
                // this.emailService.SendEmail(
                //     "lewis.renfrew@linn.co.uk",
                //     "Me",
                //     "lewis.renfrew@linn.co.uk",
                //     "Me",
                //     "Consignment Shipfile",
                //     $"Here is your pdf shipfile {emailAddress}",
                //     pdf.Result);
                
                shipfile.Message = ShipfileStatusMessages.EmailSent;
            }

            return withDetails;
        }

        private IEnumerable<ConsignmentShipfile> GetEmailDetails(IEnumerable<ConsignmentShipfile> shipfiles)
        {
            var withDetails = new List<ConsignmentShipfile>();
            
            foreach (var shipfile in shipfiles)
            {
                var data = this.shipfileRepository.FindBy(s => s.Id == shipfile.Id);
                var account = data.Consignment.SalesAccount;
                if (account.OrgId == null)
                {
                    data.Message = account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                }
                // an org
                else
                {
                    // ugly, copied from form. ask if this is still relevant?
                    if (!new List<int> { 7049, 29354, 6480 }.Contains(account.AccountId))
                    {
                        var consignmentOrderNumbers = data.Consignment.Items.Select(i => i.OrderNumber);
                        var orders = this.salesOrderRepository.FilterBy(
                            o => consignmentOrderNumbers.Contains(o.OrderNumber));

                        var outlets = orders.ToList()
                            .Select(o => o.SalesOutlet).ToList();
                    }
                }

                withDetails.Add(data);
            }

            return withDetails;
        }
    }
}
