namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Configuration;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class ConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IEmailService emailService;

        private readonly ITemplateEngine templateEngine;

        private readonly IPdfService pdfService;

        private readonly IRepository<ConsignmentShipfile, int> shipfileRepository;

        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        private readonly IConsignmentShipfileDataService dataService;

        private readonly IQueryRepository<SalesOutlet> outletRepository;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IPackingListService packingListService;

        public ConsignmentShipfileService(
            IEmailService emailService,
            ITemplateEngine templateEngine,
            IPdfService pdfService,
            IRepository<ConsignmentShipfile, int> shipfileRepository,
            IQueryRepository<SalesOrder> salesOrderRepository,
            IConsignmentShipfileDataService dataService,
            IQueryRepository<SalesOutlet> outletRepository,
            IRepository<Consignment, int> consignmentRepository,
            IPackingListService packingListService)
        {
            this.emailService = emailService;
            this.pdfService = pdfService;
            this.templateEngine = templateEngine;
            this.shipfileRepository = shipfileRepository;
            this.salesOrderRepository = salesOrderRepository;
            this.outletRepository = outletRepository;
            this.dataService = dataService;
            this.consignmentRepository = consignmentRepository;
            this.packingListService = packingListService;
        }

        public IEnumerable<ConsignmentShipfile> SendEmails(
            IEnumerable<ConsignmentShipfile> toSend,
            bool test = false,
            string testEmailAddress = null)
        {
            var withDetails = new List<ConsignmentShipfile>();
            foreach (var shipfile in toSend)
            {
                var data = this.shipfileRepository.FindById(shipfile.Id);

                if (data.ShipfileSent == "Y")
                {
                    data.Message = ShipfileStatusMessages.EmailAlreadySent;
                    withDetails.Add(data);
                    continue;
                }

                var models = this.BuildEmailModels(data);

                // A message implies there is some problem with generating the email
                if (data.Message == null)
                {
                    // potentially an email to send to each outlet in this consignment?
                    foreach (var model in models)
                    {
                        model.Subject = test ? model.ToEmailAddress : "Shipping Details";

                        var render = this.templateEngine.Render(
                            model.PdfAttachment,
                            ConfigurationManager.Configuration["SHIPFILE_TEMPLATE_PATH"]);

                        var pdf = this.pdfService.ConvertHtmlToPdf(render.Result, landscape: true);

                        this.emailService.SendEmail(
                            test ? testEmailAddress : model.ToEmailAddress,
                            model.ToCustomerName,
                            null,
                            null,
                            ConfigurationManager.Configuration["SHIPFILES_FROM_ADDRESS"],
                            "Linn Shipping",
                            model.Subject,
                            model.Body,
                            pdf.Result);
                    }

                    if (test)
                    {
                        data.Message = ShipfileStatusMessages.TestEmailSent;
                    }
                    else
                    {
                        data.Message = ShipfileStatusMessages.EmailSent;
                        data.ShipfileSent = "Y";
                    }
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
                // for an individual, send to account
                if (account.ContactDetails == null || string.IsNullOrEmpty(account.ContactDetails.EmailAddress))
                {
                    shipfile.Message = ShipfileStatusMessages.NoContactDetails;
                }
                else
                {
                    var contact = account.ContactDetails;

                    var pdfData = this.dataService.GetPdfModelData(shipfile.ConsignmentId, (int)contact.AddressId);

                    pdfData.PackingList = this.packingListService.BuildPackingList(pdfData.PackingList).ToArray();

                    var body = this.BuildEmailBody(pdfData);

                    toSend.Add(new ConsignmentShipfileEmailModel
                    {
                        PdfAttachment = pdfData,
                        ToCustomerName = contact.EmailAddress,
                        ToEmailAddress = contact.EmailAddress,
                        Body = body
                    });
                }
            }
            else
            {
                // for an org, have to decide whether to send to outlets or account

                // get all the outlets involved in the consignment
                var consignmentOrderNumbers = shipfile.Consignment.Items.Select(i => i.OrderNumber);
                var orders = this.salesOrderRepository.FilterBy(
                    o => consignmentOrderNumbers.Contains(o.OrderNumber));

                var outlets = orders.ToList()
                    .Select(o => o.SalesOutlet)
                    .GroupBy(elem => $"{elem.OutletNumber}-{elem.OrderContact.EmailAddress}")
                    .Select(group => group.First()).ToList();

                // if email address missing for one of the outlets
                if (outlets.Any(o => o.OrderContact?.EmailAddress == null))
                {
                    // if it is the only outlet for that account
                    var accountOutlets = this.outletRepository.FilterBy(
                        x => x.AccountId == shipfile.Consignment.SalesAccountId);
                    if (accountOutlets.Count() == 1)
                    {
                        var outlet =
                           accountOutlets.First();

                        // and it is the same address as the account
                        if (outlet.OutletAddressId == account.ContactDetails.AddressId && account.ContactDetails.AddressId != null)
                        {
                            // check account has contact details
                            if (string.IsNullOrEmpty(shipfile.Consignment.SalesAccount?.ContactDetails?.EmailAddress))
                            {
                                shipfile.Message = ShipfileStatusMessages.NoContactDetails;
                                return toSend;
                            }

                            // and send to account if possible
                            var pdfData = this.dataService.GetPdfModelData(shipfile.ConsignmentId, (int)account.ContactDetails.AddressId);
                            pdfData.PackingList =
                                this.packingListService.BuildPackingList(pdfData.PackingList).ToArray();
                            var body = this.BuildEmailBody(pdfData);

                            toSend.Add(new ConsignmentShipfileEmailModel
                            {
                                PdfAttachment = pdfData,
                                ToCustomerName = account.ContactDetails.EmailAddress,
                                ToEmailAddress = account.ContactDetails.EmailAddress,
                                Body = body
                            });
                        }
                    }
                    else
                    {
                        // don't send, details missing for an outlet
                        shipfile.Message = ShipfileStatusMessages.NoContactDetailsForAnOutlet;
                    }
                }
                else
                {
                    toSend.AddRange(
                        outlets.Select(o =>
                        {
                            var pdfData = this.dataService.GetPdfModelData(
                                shipfile.ConsignmentId,
                                o.OutletAddressId);
                            pdfData.PackingList = this.packingListService.BuildPackingList(pdfData.PackingList).ToArray();
                            return new ConsignmentShipfileEmailModel
                            {
                                PdfAttachment = pdfData,
                                ToEmailAddress = o.OrderContact.EmailAddress,
                                ToCustomerName = o.OrderContact.EmailAddress,
                                Body = this.BuildEmailBody(pdfData)
                            };
                        })
                        .GroupBy(x => x.ToEmailAddress)
                        .Select(x => x.FirstOrDefault()));
                }
            }

            return toSend;
        }

        private string BuildEmailBody(ConsignmentShipfilePdfModel model)
        {
            var body =
                $"Please find attached the following documents for your information {System.Environment.NewLine}{System.Environment.NewLine}";

            if (model.PackingList.Any())
            {
                body += $"Packing List {System.Environment.NewLine}";
            }

            if (model.DespatchNotes.Any())
            {
                body += $"Despatch Note/Serial Number List {System.Environment.NewLine}";
            }

            body += $"These refer to goods that left the factory on {model.DateDispatched} {System.Environment.NewLine}";

            var code = this.GetCountryCode(int.Parse(model.ConsignmentNumber));

            if (code == "E")
            {
                body += "The shipment should arrive within four working days.";
            }

            if (code == "U")
            {
                body += "The shipment should arrive tomorrow.";
            }

            return body;
        }

        private string GetCountryCode(int consignmentId)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);

            if (consignment.Address.Country.CountryCode == "GB")
            {
                return "U";
            }

            return consignment.Carrier != "TNT" ? "R" : "E";
        }
    }
}
