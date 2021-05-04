namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ConsignmentShipfileResourceBuilder : IResourceBuilder<ConsignmentShipfile>
    {
        public ConsignmentShipfileResource Build(ConsignmentShipfile shipfile)
        {
            return new ConsignmentShipfileResource
                       {
                           ConsignmentId = shipfile.ConsignmentId,
                           Status = shipfile.Message,
                           CustomerName = shipfile.Consignment.CustomerName,
                           DateClosed = shipfile.Consignment.DateClosed?.ToString("o"),
                           InvoiceNumbers = !shipfile.Consignment.Invoices.Any() ? null 
                                                : shipfile
                                                    .Consignment.Invoices.Select(i => i.DocumentNumber.ToString())
                                                    .Aggregate((acc, c) => acc + " " + c)
                       };
        }

        public string GetLocation(ConsignmentShipfile shipfile)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ConsignmentShipfile>.Build(ConsignmentShipfile shipfile) => this.Build(shipfile);

        private IEnumerable<LinkResource> BuildLinks(ConsignmentShipfile shipfile)
        {
            throw new NotImplementedException();
        }
    }
}
