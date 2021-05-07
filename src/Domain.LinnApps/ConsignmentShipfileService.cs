namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;

    using Linn.Common.Persistence;

    using MimeKit.Text;

    using SelectPdf;

    public class ConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        private readonly IEmailService emailService;

        public ConsignmentShipfileService(
            IQueryRepository<SalesOrder> salesOrderRepository,
            IEmailService emailService)
        {
            this.salesOrderRepository = salesOrderRepository;
            this.emailService = emailService;
        }

        public IEnumerable<ConsignmentShipfile> GetEmailDetails(IEnumerable<ConsignmentShipfile> shipfiles)
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
                        if (salesOrder.Account.ContactId != null)
                        {
                            shipfile.Message = null;
                        }
                        else
                        {
                            shipfile.Message = ShipfileStatusMessages.NoContactDetails;
                        }
                    }
                    else // an org
                    {

                    }
                }
            }

            return consignmentShipfiles;
        }

        public IEnumerator<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend)
        {
            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;
            var htmlString = "<style>h1 {font-size:12px;}</style><h1>Test</h1><p style='font-weight:bold'>Test Bold</p>";

            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            this.emailService.SendEmail(
                "lewis.renfrew@linn.co.uk", 
                "Me", 
                "lewis.renfrew@linn.co.uk", 
                "Me", 
                "hello", 
                "hello", 
                doc);
            doc.Close();
            throw new System.NotImplementedException();
        }
    }
}
