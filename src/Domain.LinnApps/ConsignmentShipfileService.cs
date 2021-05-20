﻿namespace Linn.Stores.Domain.LinnApps
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
                            model.Subject,
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
                        var pdf = this.dataService.BuildPdfModel(shipfile.ConsignmentId, salesOutlet.OutletAddressId);
                        var body = this.BuildEmailBody(pdf);
                        toSend.Add(new ConsignmentShipfileEmailModel
                                       {
                                           PdfAttachment = pdf,
                                           ToEmailAddress = salesOutlet.OrderContact.EmailAddress,
                                           ToCustomerName = salesOutlet.Name,
                                           Body = body
                                        });
                    }
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
