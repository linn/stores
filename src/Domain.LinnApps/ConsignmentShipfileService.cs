namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Models.Emails;

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
            var withDetails = new List<ConsignmentShipfile>();
            foreach (var shipfile in toSend)
            {
                var data = this.shipfileRepository.FindBy(s => s.Id == shipfile.Id);
                
                // what is this for
                if (shipfile.Message != null)
                {
                    continue;
                }

                var models = this.BuildPdfModels(data);

                // potentially an email to send to each outlet in this consignment
                foreach (var model in models)
                {
                    // var emailAddress = shipfile.Consignment.SalesAccount.ContactDetails?.EmailAddress;
                    var pdf = this.pdfBuilder.BuildPdf(model);
                    this.emailService.SendEmail(
                        "lewis.renfrew@linn.co.uk",
                        "Me",
                        "lewis.renfrew@linn.co.uk",
                        "Me",
                        "Consignment Shipfile",
                        $"Here is your pdf shipfile {model.ToEmailAddress}",
                        pdf.Result);
                }


                data.Message = ShipfileStatusMessages.EmailSent;
                withDetails.Add(data);
            }

            return withDetails;
        }

        private IEnumerable<ConsignmentShipfilePdfModel> BuildPdfModels(ConsignmentShipfile shipfile)
        {
            var toSend = new List<ConsignmentShipfilePdfModel>();
            
            var account = shipfile.Consignment.SalesAccount;

            // an individual, one email to be sent I imagine
            if (account.OrgId == null)
            {
                shipfile.Message = account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                toSend.Add(new ConsignmentShipfilePdfModel()); // todo
            }
            // an org, could have multiple emails to send for each outlet on consignment 
            else
            {
                var consignmentOrderNumbers = shipfile.Consignment.Items.Select(i => i.OrderNumber);
                var orders = this.salesOrderRepository.FilterBy(
                    o => consignmentOrderNumbers.Contains(o.OrderNumber));

                var outlets = orders.ToList()
                    .Select(o => o.SalesOutlet).ToList();
                foreach (var salesOutlet in outlets)
                {
                    toSend.Add(new ConsignmentShipfilePdfModel
                                   {
                                       ToEmailAddress = salesOutlet.OrderContact?.EmailAddress,
                                       ConsignmentNumber = shipfile.ConsignmentId,
                                       ToCustomerName = salesOutlet.Name,
                                       AddressLines = new[] { "Line 1", "Line 2" },
                                       PackingList = new PackingListItem[] 
                                                         { 
                                                             new PackingListItem 
                                                                 {
                                                                    ContentsDescription = "Something"
                                                                 }
                                                         }
                                   });
                }
            }

            return toSend;
        }
    }
}
