namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;

    using PuppeteerSharp;

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

        public async void SendEmails(IEnumerable<ConsignmentShipfile> toSend)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            Browser browser = await Puppeteer.LaunchAsync(new LaunchOptions
                                                              {
                                                                  Args = new[] { "--no-sandbox" },
                                                                  Headless = true
                                                              });
            Page page = await browser.NewPageAsync();
            await page.SetContentAsync("<html><head></head><body><h1>Hello World<h1></body></html>");
            var pdfStream = page.PdfStreamAsync().Result;
            await browser.CloseAsync();
            this.emailService.SendEmail(
                "lewis.renfrew@linn.co.uk",
                "Me",
                "lewis.renfrew@linn.co.uk",
                "Me",
                "hello",
                "hello",
                pdfStream);
        }
    }
}
