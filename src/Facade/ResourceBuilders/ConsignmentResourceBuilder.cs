namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ConsignmentResourceBuilder : IResourceBuilder<Consignment>
    {
        public ConsignmentResource Build(Consignment consignment)
        {
            var addressBuilder = new AddressResourceBuilder();
            var employeeBuilder = new EmployeeResourceBuilder();

            return new ConsignmentResource
                       {
                           ConsignmentId = consignment.ConsignmentId,
                           SalesAccountId = consignment.SalesAccountId,
                           Address = consignment.Address == null
                                         ? null
                                         : addressBuilder.Build(consignment.Address),
                           Status = consignment.Status,
                           Carrier = consignment.Carrier,
                           ClosedBy = consignment.ClosedBy == null 
                                          ? null
                                          : employeeBuilder.Build(consignment.ClosedBy),
                           CustomerName = consignment.CustomerName,
                           DateOpened = consignment.DateOpened.ToString("o"),
                           DateClosed = consignment.DateClosed?.ToString("o"),
                           DespatchLocationCode = consignment.DespatchLocationCode,
                           HubId = consignment.HubId,
                           ShippingMethod = consignment.ShippingMethod,
                           Terms = consignment.Terms,
                           Warehouse = consignment.Warehouse,
                           Links = this.BuildLinks(consignment).ToArray()
                       };
        }

        public string GetLocation(Consignment consignment)
        {
            return $"/logistics/consignments/{consignment.ConsignmentId}";
        }

        object IResourceBuilder<Consignment>.Build(Consignment consignment) => this.Build(consignment);

        private IEnumerable<LinkResource> BuildLinks(Consignment consignment)
        {
            yield return new LinkResource("self", this.GetLocation(consignment));

            if (consignment.HubId.HasValue)
            {
                yield return new LinkResource("hub", $"/logistics/hubs/{consignment.HubId}");
            }

            if (!string.IsNullOrEmpty(consignment.Carrier))
            {
                yield return new LinkResource("carrier", $"/logistics/carriers/{consignment.Carrier}");
            }

            if (!string.IsNullOrEmpty(consignment.Terms))
            {
                yield return new LinkResource("shipping-term", $"/logistics/shipping-terms?searchTerm={consignment.Terms}&exactOnly=true");
            }
        }
    }
}
