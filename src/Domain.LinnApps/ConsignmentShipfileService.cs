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

        public IEnumerable<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend, bool test = false)
        {
            var withDetails = new List<ConsignmentShipfile>();
            foreach (var shipfile in toSend)
            {
                var data = this.shipfileRepository.FindBy(s => s.Id == shipfile.Id);
                
                if (shipfile.Message != null || shipfile.ShipfileSent == "Y")
                {
                    continue;
                }

                var models = this.BuildEmailModels(data);

                // potentially an email to send to each outlet in this consignment?
                foreach (var model in models)
                {
                    var pdf = this.pdfBuilder.BuildPdf(model, "./views/ShipfilePdfTemplate.html");
                    this.emailService.SendEmail(
                        test ? ConfigurationManager.Configuration["SHIPFILES_TEST_ADDRESS"] : model.ToEmailAddress,
                        model.ToCustomerName,
                        null,
                        null,
                        ConfigurationManager.Configuration["SHIPFILES_FROM_ADDRESS"],
                        "Linn Shipping",
                        "Consignment Shipfile",
                        $"Here is your pdf shipfile {model.ToEmailAddress}",
                        pdf.Result);
                }

                if (!test)
                {
                    data.Message = ShipfileStatusMessages.EmailSent;
                    data.ShipfileSent = "Y";
                }

                withDetails.Add(data);
            }

            return withDetails;
        }

        private IEnumerable<ConsignmentShipfileEmailModel> BuildEmailModels(ConsignmentShipfile shipfile)
        {
            var toSend = new List<ConsignmentShipfileEmailModel>();
            
            var account = shipfile.Consignment.SalesAccount;

            if (account.OrgId == null)
            {
                if (account.ContactId == null || string.IsNullOrEmpty(account.ContactDetails.EmailAddress))
                {
                    shipfile.Message = ShipfileStatusMessages.NoContactDetails;
                }
                else
                {
                    var contact = account.ContactDetails;
                    if (contact.AddressId == null)
                    {
                        shipfile.Message = ShipfileStatusMessages.NoShippingAddress;
                    }
                    else
                    {
                        toSend.Add(this.dataService.BuildEmailModel(shipfile.ConsignmentId, (int)contact.AddressId));
                    }
                }
            }
            else
            {
                var consignmentOrderNumbers = shipfile.Consignment.Items.Select(i => i.OrderNumber);
                var orders = this.salesOrderRepository.FilterBy(
                    o => consignmentOrderNumbers.Contains(o.OrderNumber));

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
                        toSend.Add(this.dataService.BuildEmailModel(shipfile.ConsignmentId, salesOutlet.OutletAddressId));
                    }
                }
            }

            return toSend;
        }
    }
}
