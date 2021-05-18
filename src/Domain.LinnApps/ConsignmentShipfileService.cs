namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Configuration;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models.Emails;

    public class ConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IEmailService emailService;

        private readonly IPdfBuilder pdfBuilder;

        private readonly IRepository<ConsignmentShipfile, int> shipfileRepository;

        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        private readonly IConsignmentShipfileDataService dataService;

        public ConsignmentShipfileService(
            IEmailService emailService,
            IPdfBuilder pdfBuilder,
            IRepository<ConsignmentShipfile, int> shipfileRepository,
            IQueryRepository<SalesOrder> salesOrderRepository,
            IConsignmentShipfileDataService dataService)
        {
            this.emailService = emailService;
            this.pdfBuilder = pdfBuilder;
            this.shipfileRepository = shipfileRepository;
            this.salesOrderRepository = salesOrderRepository;
            this.dataService = dataService;
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

                var bccList = new Dictionary<string, string>();
                bccList.Add("name", "financeoutgoing");
                bccList.Add("address", ConfigurationManager.Configuration["SHIPFILES_OUTGOING_ADDRESS"]);


                // potentially an email to send to each outlet in this consignment
                foreach (var model in models)
                {
                    var pdf = this.pdfBuilder.BuildPdf(model, "./views/ShipfilePdfTemplate.html");
                    // this.emailService.SendEmail(
                    //     emailAddress,
                    //     model.ToCustomerName,
                    //     null,
                    //     new List<Dictionary<string, string>> { bccList },
                    //     ConfigurationManager.Configuration["SHIPFILES_FROM_ADDRESS"],
                    //     "Linn Shipping",
                    //     "Consignment Shipfile",
                    //     $"Here is your pdf shipfile {model.ToEmailAddress}",
                    //     pdf.Result);
                }

                data.Message = ShipfileStatusMessages.EmailSent;
                data.ShipfileSent = "Y";
                withDetails.Add(data);
            }

            return withDetails;
        }

        private IEnumerable<ConsignmentShipfileEmailModel> BuildPdfModels(ConsignmentShipfile shipfile)
        {
            var toSend = new List<ConsignmentShipfileEmailModel>();
            
            var account = shipfile.Consignment.SalesAccount;

            // an individual, one email to be sent I imagine
            if (account.OrgId == null)
            {
                shipfile.Message = account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                toSend.Add(new ConsignmentShipfileEmailModel()); // todo
            }
            else
            {
                // an org, could have multiple emails to send for each outlet on consignment 
                var consignmentOrderNumbers = shipfile.Consignment.Items.Select(i => i.OrderNumber);
                var orders = this.salesOrderRepository.FilterBy(
                    o => consignmentOrderNumbers.Contains(o.OrderNumber));

                // find distinct outlets to contact
                var outlets = orders.ToList()
                    .Select(o => o.SalesOutlet)
                    .GroupBy(elem => $"{elem.OutletNumber}-{elem.OrderContact.Id}")
                    .Select(group => group.First()).ToList();

                if (outlets.Any(o => o.OrderContact == null))
                {
                    shipfile.Message = account.ContactId != null ? null : ShipfileStatusMessages.NoContactDetails;
                }
                else
                {
                    foreach (var salesOutlet in outlets)
                    {
                        toSend.Add(new ConsignmentShipfileEmailModel
                                       {
                                           ToEmailAddress = salesOutlet.OrderContact?.EmailAddress,
                                           ConsignmentNumber = shipfile.ConsignmentId,
                                           ToCustomerName = salesOutlet.Name,
                                           AddressLines = new[] { "Line 1", "Line 2" },
                                           PackingList = this.dataService.GetPackingList(shipfile.ConsignmentId).ToArray(),
                                           DespatchNotes = this.dataService.GetDespatchNotes(shipfile.ConsignmentId).ToArray()
                        });
                    }
                }
            }

            return toSend;
        }
    }
}
