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

        private readonly IQueryRepository<SalesOutlet> outletRepository;

        public ConsignmentShipfileService(
            IEmailService emailService,
            IPdfBuilder pdfBuilder,
            IRepository<ConsignmentShipfile, int> shipfileRepository,
            IQueryRepository<SalesOrder> salesOrderRepository,
            IConsignmentShipfileDataService dataService,
            IQueryRepository<SalesOutlet> outletRepository)
        {
            this.emailService = emailService;
            this.pdfBuilder = pdfBuilder;
            this.shipfileRepository = shipfileRepository;
            this.salesOrderRepository = salesOrderRepository;
            this.outletRepository = outletRepository;
            this.dataService = dataService;
        }

        public IEnumerable<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend, bool test = false)
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
                        model.Subject = "Shipping Details";
                        var pdf = this.pdfBuilder.BuildPdf(model.PdfAttachment, "./views/ShipfilePdfTemplate.html");
                        this.emailService.SendEmail(
                            test ? ConfigurationManager.Configuration["SHIPFILES_TEST_ADDRESS"] : model.ToEmailAddress,
                            model.ToCustomerName,
                            null,
                            null,
                            ConfigurationManager.Configuration["SHIPFILES_FROM_ADDRESS"],
                            "Linn Shipping",
                            test ? model.ToEmailAddress : model.Subject,
                            model.Body,
                            pdf.Result);
                    }

                    if (!test)
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
                if (account.ContactId == null || string.IsNullOrEmpty(account.ContactDetails.EmailAddress))
                {
                    shipfile.Message = ShipfileStatusMessages.NoContactDetails;
                }
                else
                {
                    var contact = account.ContactDetails;
                
                    var pdf = this.dataService.BuildPdfModel(shipfile.ConsignmentId, (int)contact.AddressId);
                    var body = this.BuildEmailBody(pdf);

                        toSend.Add(new ConsignmentShipfileEmailModel
                                       {
                                           PdfAttachment = pdf,
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
                            var pdf = this.dataService.BuildPdfModel(shipfile.ConsignmentId, (int)account.ContactDetails.AddressId);
                            var body = this.BuildEmailBody(pdf);

                            toSend.Add(new ConsignmentShipfileEmailModel
                                           {
                                               PdfAttachment = pdf,
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
                    // multiple outlets to email, email addresses present and correct
                    toSend.AddRange(
                        from salesOutlet in outlets 
                        let pdf = this.dataService.BuildPdfModel(shipfile.ConsignmentId, salesOutlet.OutletAddressId) 
                        let body = this.BuildEmailBody(pdf) 
                        select new ConsignmentShipfileEmailModel
                                   {
                                       PdfAttachment = pdf, 
                                       ToEmailAddress = salesOutlet.OrderContact.EmailAddress, 
                                       ToCustomerName = salesOutlet.OrderContact.EmailAddress,
                                Body = body
                                   });
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

            // todo - something about when shipment should arrive?
            return body;
        }
    }
}
