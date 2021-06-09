namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    public class ConsignmentShipfileResourceBuilder : IResourceBuilder<ConsignmentShipfile>
    {
        public ConsignmentShipfileResource Build(ConsignmentShipfile shipfile)
        {
            var invoices = shipfile.Consignment.Invoices;
            return new ConsignmentShipfileResource
                       {
                           Id = shipfile.Id,
                           ConsignmentId = shipfile.ConsignmentId,
                           Status = shipfile.Message,
                           CustomerName = shipfile.Consignment.CustomerName,
                           DateClosed = shipfile.Consignment.DateClosed?.ToString("o"),
                           InvoiceNumbers = invoices != null && invoices.Any() ? shipfile.Consignment.Invoices.Select(i => i.DocumentNumber.ToString())
                               .Aggregate((acc, c) => acc + " " + c) : null
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
