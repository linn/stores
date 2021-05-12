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

        public ConsignmentShipfileService(
            IEmailService emailService,
            IShipfilePdfBuilder pdfBuilder,
            IRepository<ConsignmentShipfile, int> shipfileRepository)
        {
            this.emailService = emailService;
            this.pdfBuilder = pdfBuilder;
            this.shipfileRepository = shipfileRepository;
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
            var withDetails = new List<ConsignmentShipfile>();
            
            foreach (var shipfile in shipfiles)
            {
                var data = this.shipfileRepository.FindBy(s => s.Id == shipfile.Id);
                var account = data.Consignment.Items.First().SalesOrder.Account;
                if (account.OrgId == null)
                {
                    data.Message = account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                }
                else
                {
                    data.Message = null;
                }

                withDetails.Add(data);
            }

            return withDetails;
        }
    }
}
