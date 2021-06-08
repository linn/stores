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
            return new ConsignmentResource
                       {
                           AddressId = consignment.AddressId,
                           ConsignmentId = consignment.ConsignmentId,
                           CountryCode = consignment.Address?.Country?.CountryCode,
                           Country = consignment.Address?.Country?.DisplayName,
                           SalesAccountId = consignment.SalesAccountId,
                           
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
        }
    }
}
